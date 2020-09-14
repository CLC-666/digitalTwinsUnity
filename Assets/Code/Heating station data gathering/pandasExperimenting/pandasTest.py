import pandas as pd
import plotly.express as px

from plotly.subplots import make_subplots
import plotly.graph_objects as go

power_df = pd.read_csv("power.csv")
power_df = power_df.drop([0])

power_df.head()

heating_df = pd.read_csv('heating.csv')
heating_df


fig = px.scatter(power_df, x="World Time", y="Active Power Ext")
fig.show()


df = pd.DataFrame({
    'time_power':power_df["World Time"],
    'power':power_df["Active Power Ext"],
    'time_temp':heating_df["World Time"],
    'temp':heating_df['Actual Heat']
})
df_int = df[1600:1800]

from datetime import datetime

datetime.strptime(df.time_power[1600], '%M:%S.%S')



df.head()

fig = px.scatter(df_int, x="power", y="temp")
fig.show(renderer="browser")

fig = make_subplots(rows=2, cols=1)

fig.add_trace(
    go.Scatter(x=df_int.time, y=df_int.power),
    row=1, col=1
)

fig.add_trace(
    go.Scatter(x=df_int.time, y=df_int.temp),
    row=2, col=1
)
fig.update_layout(height=800)
fig.show(renderer="browser")
