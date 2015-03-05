# ElasticHealthMonitor
VB.net app that calls URL of elastic DB with health call and parses te returned JSON response to look at the status of the cluster.  if not green then it alerts you via email.  quite simple app with no error logging as I needed something quick to alert me of the status of my cluster.

Needs 4 Parameters
1) Elastic DB URL eg http://192.168.1.1:9200/_cluster/health?pretty=true
2) Email address to send the alert to (only 1 email address)
3) SMTP Server Address
4) SendWhenGreen  this will be 0 or 1 - 1 indicates to send email out even when the status is green.
