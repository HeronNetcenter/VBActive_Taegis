# EVENTS - CONTAGEM DOS EVENTOS DE UM CLIENTE NAS ULTIMAS 24HS
#          VERSÃO SDK/OAuth2 PARA SER USADA NA PRODUÇÃO DENTRO DE PROGRAMAS VB COM PARÂMETROS
""" 
AUTOR: HERON JR
DATA:  29/08/23
ALT:   
US2: https://api.delta.taegis.secureworks.com
Tenant Id: 137287
Tenant Name: Netcenter - Production
client_id: SGDErpvNZHWbG5hhRVTQ1uJ3Tl8TExMg
client_secret: ZVvI3BUTRlpgkjs9D9e4wgex9T6_FcZrmVzUVYMJIwJnx9LjzRIqb5hJwtyVRZxx
 """
 
 # ======================= Variáveis de importação =======================
import logging
import os
import sys

from taegis_sdk_python import GraphQLService
from taegis_sdk_python.authentication import get_token_by_oauth
from taegis_sdk_python._consts import TAEGIS_ENVIRONMENT_URLS
from taegis_sdk_python.services.events.types import (EventQueryOptions, EventQueryResults)
# ========================================================================================

# ======================= Disable Deprecated Commands Messages =======================
logging.getLogger("taegis_sdk_python.utils").setLevel(logging.ERROR)
logging.getLogger("taegis_sdk_python.services").setLevel(logging.ERROR)
# ========================================================================================

# DEFINIÇÃO DOS PARÂMETROS EXTERNOS
# pClientName: Nome do cliente
# pTenantId: Tenant ID do cliente
# O processo será automatizado pelo programa VB.Net de chamada que calculará a data do dia seguinte a partir da data informada como parâmetro.
import argparse
parser = argparse.ArgumentParser()
parser.add_argument('pClientName')
parser.add_argument('pEnvironment')
parser.add_argument('pTenantId')
parser.add_argument('pClientId')
parser.add_argument('pClientSecret')

args = parser.parse_args()

# ======================= Client Credentials =======================
# tenant settings
client = args.pClientName
env = args.pEnvironment
tenant_id = args.pTenantId
client_id = args.pClientId
client_secret = args.pClientSecret

# set request_url according to environment
request_url = TAEGIS_ENVIRONMENT_URLS[env]

# get access_token using OAuth2 credentials
access_token = get_token_by_oauth(
    request_url=request_url,
    client_id=client_id,
    client_secret=client_secret
)

service = GraphQLService(
    tenant_id=tenant_id,
    environment=env
)
# =============================================================================================

# ======================= Controle de Datas =======================
import datetime
from datetime import timedelta
from datetime import date
import calendar

d = datetime.date.today()
dia_01_mes_passado = (d - timedelta(days=d.day)).replace(day=1)
mes_passado = d.month
ano_mes_passado = d.year
_, dia_ult_mes_passado = calendar.monthrange(ano_mes_passado, mes_passado)
dia_ult_mes_passado = date(ano_mes_passado, mes_passado, 1) + timedelta(days=-1)

# ======================= Query =======================
with service(access_token=access_token):
    options = EventQueryOptions(
        timestamp_ascending=True,
        page_size=1000,
        max_rows=100000,
        skip_cache=True,
        aggregation_off=False,
    )
    xquery = f"FROM process EARLIEST='{dia_01_mes_passado}T00:00:00' AND LATEST='{dia_ult_mes_passado}T00:00:00' | aggregate count"

    result = service.events.subscription.event_query(
        xquery,
        options=options
    )

# ======================= Query - Resultado Impresso  =======================
    for query_result in result:
        for event in query_result.result.rows:
            print(f"Contagem de eventos no mês anterior - Cliente {client}: {event['row_count']}")
            events_count = event['row_count']

# ======================= Query - Resultado Gravado em Arquivo CSV  =======================
    arquivo = open('events_count_' + tenant_id + '_' + dia_01_mes_passado.strftime('%Y%m%d') + '_' + 
                   dia_ult_mes_passado.strftime('%Y%m%d') + '.csv', 'w')
    arquivo.write(dia_01_mes_passado.strftime('%Y%m%d') + ';' + dia_ult_mes_passado.strftime('%Y%m%d') + ';' + str(events_count))
    arquivo.close()

