# EVENTS - CONTAGEM DOS EVENTOS DE UM CLIENTE NO DIA ANTERIOR
#          VERSÃO SDK/OAuth2 PARA SER USADA NA PRODUÇÃO DENTRO DE PROGRAMAS VB SEM PARÂMETROS
""" 
AUTOR: HERON JR
DATA:  29/08/23
ALT:   05/06/24-18/12/24
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

# # ======================= Credentials NETCENTER =======================
# # tenant settings
# client = 'NETCENTER'
# env = 'delta'
# tenant_id = '137287'

# # hardcode credentials
# client_id = 'SGDErpvNZHWbG5hhRVTQ1uJ3Tl8TExMg'
# client_secret = 'ZVvI3BUTRlpgkjs9D9e4wgex9T6_FcZrmVzUVYMJIwJnx9LjzRIqb5hJwtyVRZxx'

# # ======================= Credentials NORTEC =======================
# # tenant settings
# client = 'NORTEC'
# env = 'delta'
# tenant_id = '138529'

# # hardcode credentials
# client_id = 'p0redWh1UyYnJxmaL6YYoCbmHBuf4vLA'
# client_secret = 'yc5vFC_RFsFQzaYk_uc9thToqBagQaLEPZ-Hf4JJMZF4NuReNY6--eDlD9SvZDf-'

# # ======================= Credentials TECHNOS =======================
# # tenant settings
client = 'TECHNOS'
env = 'delta'
tenant_id = '142779'

# # hardcode credentials
client_id = '2rvSMDA40yFD5BlnN05qzrJQ6tw1xP1P'
client_secret = 'f9Hl-QbK3D6BOBcToyNGuq6lAVCzio85GszUnpp9lw98iRx-5_H5oOWQAaUkYA3G'

# ======================= Credentials LOPES BSJ ======================= (05/06/24)
# tenant settings
# client = 'LOPES BSJ'
# env = 'delta'
# tenant_id = '146127'

# hardcode credentials
# client_id = 'fAJGVCs32Cq0vSJW5D9LsyNAlwNZ6z5U'
# client_secret = 'U2sGHnwwN6JH1-15E23vstUV6kUk5Co0MOXnd_gCBhHkk62PsXzK2ap6MDNg6uJV'

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

# Teste de datas para definir o período do dia de ontem - 18/12/2024
dia_01_mes_atual = date.today().replace(day=1)
dia_hoje = date.today()
dia_ontem = date.today() - timedelta(days=1)

print(f"Primeiro do mês ===> {dia_01_mes_atual}")
print(f"Hoje ===> {dia_hoje}")
print(f"Ontem ===> {dia_ontem}")

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
    # xquery = f"FROM process EARLIEST='{dia_01_mes_passado}T00:00:00' AND LATEST='{dia_ult_mes_passado}T00:00:00' | aggregate count"
    xquery = f"FROM process EARLIEST='{dia_ontem}T00:00:00' AND LATEST='{dia_hoje}T00:00:00' | aggregate count"

    result = service.events.subscription.event_query(
        xquery,
        options=options
    )

# ======================= Query - Resultado Impresso  =======================
    for query_result in result:
        for event in query_result.result.rows:
            strRow_count=event['row_count']
            strRow_count=f'{strRow_count:_.0f}'
            strRow_count=strRow_count.replace(".",",").replace("_",".")
            # print(f"Contagem de eventos no mês ({dia_01_mes_passado.strftime('%d-%m-%Y')} a {dia_ult_mes_passado.strftime('%d-%m-%Y')}) - Cliente {client}: {event['row_count']:,.0f}")
            # print(f"Contagem de eventos no mês ({dia_01_mes_passado.strftime('%d-%m-%Y')} a {dia_ult_mes_passado.strftime('%d-%m-%Y')}) - Cliente {client}: {strRow_count}")
            print(f"Contagem de eventos no dia ({dia_ontem.strftime('%d-%m-%Y')} a {dia_ontem.strftime('%d-%m-%Y')}) - Cliente {client}: {strRow_count}")
            events_count = event['row_count']

# ======================= Query - Resultado Gravado em Arquivo CSV  =======================
    # arquivo = open('events_count_' + tenant_id + '_' + dia_01_mes_passado.strftime('%Y%m%d') + '_' + 
    #                dia_ult_mes_passado.strftime('%Y%m%d') + '.csv', 'w')
    # arquivo.write(dia_01_mes_passado.strftime('%Y%m%d') + ';' + dia_ult_mes_passado.strftime('%Y%m%d') + ';' + str(events_count))

    arquivo = open('events_count_' + tenant_id + '_' + dia_ontem.strftime('%Y%m%d') + '_' + 
                   dia_ontem.strftime('%Y%m%d') + '.csv', 'w')
    arquivo.write(dia_ontem.strftime('%Y%m%d') + ';' + dia_ontem.strftime('%Y%m%d') + ';' + str(events_count))

    arquivo.close()

