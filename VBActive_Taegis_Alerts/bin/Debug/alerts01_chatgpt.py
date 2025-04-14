# ALERTS - RECUPERA OS ALERTAS DE UM CLIENTE DO DIA ANTERIOR
""" 
AUTOR: HERON JR
DATA:  24/01/23
ALT:   26/01/23-03/04/23
       27/12/24: INCLUSÃO DO CAMPO FIRST_RESOLVED_AT
       13/03/25: AMPLIAÇÃO DO PERÍODO DE COLETA - SUBSTITUI "yesterday" POR UM PERÍODO ANTERIOR DE 3 MESES first_day_3_months_ago
       16/03/25: CONTINUAM OS TESTES COM CHATGPT-IA PARA PERÍODOS MENORES DO QUE 3 MESES
US2: https://api.delta.taegis.secureworks.com
client_name	tenant_id	  client_id	                        client_secret
NETCENTER   137287	    SGDErpvNZHWbG5hhRVTQ1uJ3Tl8TExMg	ZVvI3BUTRlpgkjs9D9e4wgex9T6_FcZrmVzUVYMJIwJnx9LjzRIqb5hJwtyVRZxx
NORTEC	    138529	    p0redWh1UyYnJxmaL6YYoCbmHBuf4vLA	yc5vFC_RFsFQzaYk_uc9thToqBagQaLEPZ-Hf4JJMZF4NuReNY6--eDlD9SvZDf-
TECHNOS	    142779	    2rvSMDA40yFD5BlnN05qzrJQ6tw1xP1P	f9Hl-QbK3D6BOBcToyNGuq6lAVCzio85GszUnpp9lw98iRx-5_H5oOWQAaUkYA3G
 """

# Setup dependencies
from oauthlib.oauth2 import BackendApplicationClient
from requests_oauthlib import OAuth2Session
import json
import os

# DEFINIÇÃO DOS PARÂMETROS EXTERNOS
# pClientName: Nome do cliente
# pTenantId: Tenant ID do cliente
# O processo será automatizado pelo programa VB.Net de chamada que calculará a data do dia seguinte a partir da data informada como parâmetro.

import argparse
parser = argparse.ArgumentParser()
parser.add_argument('pTenantId')
parser.add_argument('pClientId')
parser.add_argument('pClientSecret')
parser.add_argument('pFirstDayLastMonth')
args = parser.parse_args()

FIRST_DAY = args.pFirstDayLastMonth
TENANT_ID = args.pTenantId
CLIENT_ID = args.pClientId
CLIENT_SECRET = args.pClientSecret

# ==========================================================================================
# PARA TESTES - COMENTAR ACIMA DESDE import argparse ATÉ CLIENTE_SECRET = args.pClientSecret
# E TIRAR COMENTÁRIOS DO CLIENTE ESCOLHIDO NAS LINHAS ABAIXO:
# ==========================================================================================
# FIRST_DAY = '1'

# TENANT = 'NETCENTER'
# TENANT_ID = "137287"
# CLIENT_ID = "SGDErpvNZHWbG5hhRVTQ1uJ3Tl8TExMg"
# CLIENT_SECRET = "ZVvI3BUTRlpgkjs9D9e4wgex9T6_FcZrmVzUVYMJIwJnx9LjzRIqb5hJwtyVRZxx"

# TENANT = 'NORTEC'
# TENANT_ID = '138529'
# CLIENT_ID = 'p0redWh1UyYnJxmaL6YYoCbmHBuf4vLA'
# CLIENT_SECRET = 'yc5vFC_RFsFQzaYk_uc9thToqBagQaLEPZ-Hf4JJMZF4NuReNY6--eDlD9SvZDf-'

# TENANT = 'TECHNOS'
# TENANT_ID = '142779'
# CLIENT_ID = '2rvSMDA40yFD5BlnN05qzrJQ6tw1xP1P'
# CLIENT_SECRET = 'f9Hl-QbK3D6BOBcToyNGuq6lAVCzio85GszUnpp9lw98iRx-5_H5oOWQAaUkYA3G'

# TENANT = 'LOPES_BSJ'
# TENANT_ID = '146127'
# CLIENT_ID = 'fAJGVCs32Cq0vSJW5D9LsyNAlwNZ6z5U'
# CLIENT_SECRET = 'U2sGHnwwN6JH1-15E23vstUV6kUk5Co0MOXnd_gCBhHkk62PsXzK2ap6MDNg6uJV'

# TENANT = 'INC'
# TENANT_ID = '148351'
# CLIENT_ID = 'QJsiirGqZAcTIvHbfZoeJRe3LgYk1zuw'
# CLIENT_SECRET = 'hxaIGOtB4vqx06rhhSADCqanTSI25K7rFRXcWcrtcuJLglw_t5rTSt0XyIshM2P3'

# TENANT = 'PINHEIRO GUIMARAES'
# TENANT_ID = '148507'
# CLIENT_ID = 'M0qQ4hBGyfvz81JYAL7789rZQDkkd05Q'
# CLIENT_SECRET = '3FjyFdScwUWhWVRgvsSAgE3blHSJ8hcwQPrwq94czP_uqS-zka8SeHB8jnydMRgo'

API_ENDPOINT='https://api.delta.taegis.secureworks.com'

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
          first_resolved_at {
            seconds
          }
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

# ====================================================== ÁREA ANTERIOR A 14/03/2025 ======================================================
auth = "Bearer " + token['access_token']

# Get current date
from datetime import date, datetime, timedelta
from dateutil.relativedelta import relativedelta

today = date.today()

# Get the first and the last days of the previous month for the process
if FIRST_DAY == '1':
  first_day_last_month = datetime(today.year, today.month - 1, 1) if today.month > 1 else datetime(today.year - 1, 12, 1)
  last_day_last_month = datetime(today.year, today.month - 1, 10) if today.month > 1 else datetime(today.year - 1, 12, 10)
elif FIRST_DAY == '11':
  first_day_last_month = datetime(today.year, today.month - 1, 11) if today.month > 1 else datetime(today.year - 1, 12, 1)
  last_day_last_month = datetime(today.year, today.month - 1, 20) if today.month > 1 else datetime(today.year - 1, 12, 20)
elif FIRST_DAY == '21':
  first_day_last_month = datetime(today.year, today.month - 1, 21) if today.month > 1 else datetime(today.year - 1, 12, 21)
  last_day_last_month = datetime(today.year, today.month, 1) - timedelta(days=1)

# Format the dates to the required string format
cql_query = f"EARLIEST= '{first_day_last_month.strftime('%Y-%m-%d')}T00:00:00.000Z' AND LATEST= '{last_day_last_month.strftime('%Y-%m-%d')}T23:59:59.000Z'"

# EM CASO DE TESTE DESCOMENTAR ABAIXO
# print(TENANT)
# print(cql_query)

# Initialize variables for pagination
offset = 0
limit = 10000
total_records = 0
all_alerts = []

while True:
    params = {
        "searchRequestInput": {
            "cql_query": cql_query,
            "limit": limit,
            "offset": offset  # Pagination offset
        }
    }

    # Make the API request
    r = oauth_client.post(f'{API_ENDPOINT}/graphql', json={
            "query": alertsQuery,
            "variables": params,
        }, headers={"X-Tenant-Context": TENANT_ID, "Authorization" : auth})

    # Decode the response
    result = json.loads(r.content)

    # Check if we have alerts in the response
    if 'data' in result and 'alertsServiceSearch' in result['data']:
        alerts_data = result['data']['alertsServiceSearch']
        if 'alerts' in alerts_data and 'list' in alerts_data['alerts']:
            alerts_list = alerts_data['alerts']['list']
            all_alerts.extend(alerts_list)
            total_records += len(alerts_list)
            print(f"Retrieved {len(alerts_list)} alerts, Total so far: {total_records}")

            # If we retrieved less than the limit, it means we have all records
            if len(alerts_list) < limit:
                break
            else:
                # Move to the next batch of alerts
                offset += limit
        else:
            print("No alerts found in response")
            break
    else:
        print(f"Error: {result.get('errors', 'Unknown error')}")
        break

# Save the results to a JSON file
if all_alerts:
    arquivo = open(f'alerts01_{TENANT_ID}_{first_day_last_month.strftime("%Y%m%d")}.json', 'w')
    arquivo.write(json.dumps({"alerts": all_alerts}, indent=4))
    arquivo.close()

# print(f"Total records retrieved: {total_records}")
# ====================================================== ÁREA ANTERIOR A 14/03/2025 ======================================================
