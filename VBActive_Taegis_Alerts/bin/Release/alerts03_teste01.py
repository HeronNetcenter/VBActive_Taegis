# ALERTS - RECUPERA OS ALERTAS DE UM CLIENTE DO DIA ANTERIOR
""" 
AUTOR: HERON JR
DATA:  24/01/23
ALT:   26/01/23-03/04/23
US2: https://api.delta.taegis.secureworks.com
Tenant Id: 137287
Tenant Name: Netcenter - Production
client_id: SGDErpvNZHWbG5hhRVTQ1uJ3Tl8TExMg
client_secret: ZVvI3BUTRlpgkjs9D9e4wgex9T6_FcZrmVzUVYMJIwJnx9LjzRIqb5hJwtyVRZxx
 """

# Setup dependencies
from oauthlib.oauth2 import BackendApplicationClient
from requests_oauthlib import OAuth2Session
import json
import os

API_ENDPOINT='https://api.delta.taegis.secureworks.com'
# TENANT_ID = '137287'
# CLIENT_ID = 'SGDErpvNZHWbG5hhRVTQ1uJ3Tl8TExMg'
# CLIENT_SECRET = 'ZVvI3BUTRlpgkjs9D9e4wgex9T6_FcZrmVzUVYMJIwJnx9LjzRIqb5hJwtyVRZxx'

# NORTEC
# TENANT_ID = '138529'
# CLIENT_ID = 'p0redWh1UyYnJxmaL6YYoCbmHBuf4vLA'
# CLIENT_SECRET = 'yc5vFC_RFsFQzaYk_uc9thToqBagQaLEPZ-Hf4JJMZF4NuReNY6--eDlD9SvZDf-'

# TECHNOS
TENANT_ID = '142779'
CLIENT_ID = '2rvSMDA40yFD5BlnN05qzrJQ6tw1xP1P'
CLIENT_SECRET = 'f9Hl-QbK3D6BOBcToyNGuq6lAVCzio85GszUnpp9lw98iRx-5_H5oOWQAaUkYA3G'

# Fetch and test client credentials
client = BackendApplicationClient(client_id=CLIENT_ID)
oauth_client = OAuth2Session(client=client)
token = oauth_client.fetch_token(token_url=f'{API_ENDPOINT}/auth/api/v2/auth/token', client_id=CLIENT_ID, client_secret=CLIENT_SECRET)

# testing access
r = oauth_client.get(f"{API_ENDPOINT}/assets/version")
print(r.content)

# Search Alerts.
alertsQuery = """

query ($searchRequestInput: SearchRequestInput)
{
  alertsServiceSearch(in: $searchRequestInput) {
    status
    reason
    alerts {
      list {
        enrichment_details {
          mitre_attack_info {
            tactics
            technique_id
          }
        }
        id
        tenant_id
        status
        suppressed
        suppression_rules {
          id
          version
        }
        resolution_reason
        attack_technique_ids
        entities{
          entities
          relationships{
            from_entity
            relationship
            to_entity
          }
        }
        metadata {
          engine {
            name
          }
          creator {
            detector {
              version
              detector_id
            }
            rule {
              rule_id
              version
            }
          }
          title
          description
          confidence
          severity
          created_at {
            seconds
          }
        }
        investigation_ids {
          id
        }
        sensor_types
      }
      total_results
    }
  }
}

"""

auth = "Bearer " + token['access_token']

# Parâmetros para o dia anterior (24 horas)
from datetime import datetime, timedelta
yesterday = datetime.now() - timedelta(days=500)

print("EARLIEST= '" + yesterday.strftime('%Y-%m-%d') + 'T00:00:00.000Z' + "' AND LATEST= '" + datetime.now().strftime('%Y-%m-%d') + 'T23:00:00.000Z' + "'")
params = {
    "searchRequestInput": {
        "cql_query": "EARLIEST= '" + yesterday.strftime('%Y-%m-%d') + 'T00:00:00.000Z' + "' AND LATEST= '" + datetime.now().strftime('%Y-%m-%d') + 'T23:00:00.000Z' + "'",
        "limit": 1000000
    }
}

print(yesterday.strftime('%Y-%m-%d') + 'T00:00:00.000Z')
print(datetime.now().strftime('%Y-%m-%d') + 'T00:00:00.000Z')

r = oauth_client.post(f'{API_ENDPOINT}/graphql', json={
        "query": alertsQuery,
        "variables": params,
    }, headers={"X-Tenant-Context": TENANT_ID, "Authorization" : auth})


# We can now decode the response and print it out
result = json.loads(r.content)

# Imprime o resultado - PODE DEMORAR MUITO!
# print(json.dumps(result, indent=4))
arquivo = open('alerts_500d_' + TENANT_ID + '_' + yesterday.strftime('%Y%m%d') + '.json', 'w')
arquivo.write(json.dumps(result, indent=4))
arquivo.close()