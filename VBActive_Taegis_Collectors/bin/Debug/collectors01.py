# collectors01
""" 
AUTOR: HERON JR
DATA:  06/03/23
ALT:   19/04/23
US2: https://api.delta.taegis.secureworks.com
Tenant Id: 137287
Tenant Name: Netcenter - Production
client_id: SGDErpvNZHWbG5hhRVTQ1uJ3Tl8TExMg
client_secret: ZVvI3BUTRlpgkjs9D9e4wgex9T6_FcZrmVzUVYMJIwJnx9LjzRIqb5hJwtyVRZxx
 """

from oauthlib.oauth2 import BackendApplicationClient
from requests_oauthlib import OAuth2Session
from graphqlclient import GraphQLClient

import os
import json
import pprint

# DEFINIÇÃO DOS PARÂMETROS EXTERNOS - 19/04/23
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

# We need to first set up our client token and GraphQL client instance
pp = pprint.PrettyPrinter(depth=6)

# client_id='SGDErpvNZHWbG5hhRVTQ1uJ3Tl8TExMg'
# client_secret='ZVvI3BUTRlpgkjs9D9e4wgex9T6_FcZrmVzUVYMJIwJnx9LjzRIqb5hJwtyVRZxx'
client = BackendApplicationClient(client_id=CLIENT_ID)
oauth_client = OAuth2Session(client=client)
token = oauth_client.fetch_token(token_url='https://api.delta.taegis.secureworks.com/auth/api/v2/auth/token', client_id=CLIENT_ID,
                              client_secret=CLIENT_SECRET)

gql_client = GraphQLClient('https://api.delta.taegis.secureworks.com/graphql')
gql_client.inject_token("Bearer " + token['access_token'], "Authorization")

# # Let's start off by creating a new collector
# createCollectorQuery = gql_client.execute(
# '''
# mutation {
#   createCluster(
#     clusterInput: {
#       name: "sample-collector"
#       description: "a collector created from a script!"
#       network: { dhcp: true, hostname: "sample-collector-host" }
#     }
#   ) {
#     createdAt
#     id
#     name
#     description
#     network {
#       dhcp
#       hostname
#     }
#   }
# }
# '''
# )

# # We can now decode the response and print it out
# result = json.loads(createCollectorQuery)
# # Barring any errors, we should the our new created 'sample-collector' information printed out
# pp.pprint(result)

# Now let's try to list all of our created collectors
getAllClustersQuery = gql_client.execute(
'''
query {
  getAllClusters(role: "collector") {
    createdAt
    updatedAt
    id
    role
    name
    type
    clusterType
    description
    health
    network {
      dhcp
      hostname
      dns
      ntp
      proxy
    }
    deployments {
      name
      id
      config
    }
    status {
      id
      createdAt
    }
  }
}
''')

# We can now decode the response and print it out
result = json.loads(getAllClustersQuery)
# Barring any errors, we should see our newly created 'sample-collector' listed
pp.pprint(result)

# Parâmetros para o dia anterior (24 horas)
from datetime import datetime, timedelta
# yesterday = datetime.now() - timedelta(days=1)

arquivo = open('collectors01_' + TENANT_ID + '_' + datetime.now().strftime('%Y%m%d') + '.json', 'w')
arquivo.write(json.dumps(result, indent=4))
arquivo.close()