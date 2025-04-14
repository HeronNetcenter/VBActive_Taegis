# ASSETS SEARCH
""" 
AUTOR: HERON JR
DATA:  27/01/23
ALT:   27/01/23-17/04/23
US2: https://api.delta.taegis.secureworks.com
Tenant Id: 137287
Tenant Name: Netcenter - Production
client_id: SGDErpvNZHWbG5hhRVTQ1uJ3Tl8TExMg
client_secret: ZVvI3BUTRlpgkjs9D9e4wgex9T6_FcZrmVzUVYMJIwJnx9LjzRIqb5hJwtyVRZxx
 """
from oauthlib.oauth2 import BackendApplicationClient
from requests_oauthlib import OAuth2Session
import json
import os

# DEFINIÇÃO DOS PARÂMETROS EXTERNOS - 17/04/23
# pClientName: Nome do cliente
# pTenantId: Tenant ID do cliente
# O processo será automatizado pelo programa VB>Net de chamada que calculará a data do dia seguinte a partir da data informada como parâmetro.
import argparse
parser = argparse.ArgumentParser()
parser.add_argument('pTenantId')
parser.add_argument('pClientId')
parser.add_argument('pClientSecret')
args = parser.parse_args()

TENANT_ID = args.pTenantId
CLIENT_ID = args.pClientId
CLIENT_SECRET = args.pClientSecret

# client_id='SGDErpvNZHWbG5hhRVTQ1uJ3Tl8TExMg'
# client_secret='ZVvI3BUTRlpgkjs9D9e4wgex9T6_FcZrmVzUVYMJIwJnx9LjzRIqb5hJwtyVRZxx'
client = BackendApplicationClient(client_id=CLIENT_ID)
oauth_client = OAuth2Session(client=client)
token = oauth_client.fetch_token(token_url='https://api.delta.taegis.secureworks.com/auth/api/v2/auth/token', client_id=CLIENT_ID,
                                 client_secret=CLIENT_SECRET)

# Show me the first 100 assets (All) - options are All | Active | Deleted
allAssetsQuery = '''
{
  allAssets(
    offset: 0,
    limit: 10000,
    order_by: hostname,
    filter_asset_state: All
  )
  {
    totalResults
    assets {
      id
      hostId
      sensorTenant
      sensorId
      sensorVersion
      osFamily
      endpointType
      hostnames {
        hostname
      }
      osVersion
    }
  }
}
'''

result = oauth_client.post('https://api.delta.taegis.secureworks.com/graphql',
                           json={"query": allAssetsQuery})

print(result.content)

# Parâmetros para o dia anterior (24 horas)
from datetime import datetime, timedelta
# yesterday = datetime.now() - timedelta(days=1)

arquivo = open('assets01_' + TENANT_ID + '_' + datetime.now().strftime('%Y%m%d') + '.json', 'wb')
arquivo.write(result.content,)
arquivo.close()
