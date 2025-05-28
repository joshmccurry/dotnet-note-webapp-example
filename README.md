# Demistifying Linux Webjobs

## App Service Current Linux Webapp Runtimes
```json
{
  "linux": [
    "DOTNETCORE:9.0",
    "DOTNETCORE:8.0",
    "NODE:22-lts",
    "NODE:20-lts",
    "NODE:18-lts",
    "PYTHON:3.13",
    "PYTHON:3.12",
    "PYTHON:3.11",
    "PYTHON:3.10",
    "PYTHON:3.9",
    "PHP:8.4",
    "PHP:8.3",
    "PHP:8.2",
    "PHP:8.1",
    "JAVA:21-java21",
    "JAVA:17-java17",
    "JAVA:11-java11",
    "JAVA:8-jre8",
    "JBOSSEAP:8-java17",
    "JBOSSEAP:8-java11",
    "JBOSSEAP:8-java17_byol",
    "JBOSSEAP:8-java11_byol",
    "JBOSSEAP:7-java17",
    "JBOSSEAP:7-java11",
    "JBOSSEAP:7-java8",
    "JBOSSEAP:7-java17_byol",
    "JBOSSEAP:7-java11_byol",
    "JBOSSEAP:7-java8_byol",
    "TOMCAT:11.0-java21",
    "TOMCAT:11.0-java17",
    "TOMCAT:11.0-java11",
    "TOMCAT:10.1-java21",
    "TOMCAT:10.1-java17",
    "TOMCAT:10.1-java11",
    "TOMCAT:10.0-java17",
    "TOMCAT:10.0-java11",
    "TOMCAT:10.0-jre8",
    "TOMCAT:9.0-java21",
    "TOMCAT:9.0-java17",
    "TOMCAT:9.0-java11",
    "TOMCAT:9.0-jre8",
    "TOMCAT:8.5-java11",
    "TOMCAT:8.5-jre8"
  ]
}
```
## Helpful AZ Cli Commands

### Continuous WebJobs

| Command | Description |
| ----------- | ----------- |
| `az webapp webjob continuous` | Allows management operations of continuous webjobs on a web app. |
| `az webapp webjob continuous list` | List all continuous webjobs on a selected web app. |
| `az webapp webjob continuous remove` | Delete a specific continuous webjob. |
| `az webapp webjob continuous start` | Start a specific continuous webjob on a selected web app. |
| `az webapp webjob continuous stop` | Stop a specific continuous webjob. |

### Triggered WebJobs

| Command | Description |
| ----------- | ----------- |
| `az webapp webjob triggered` | Allows management operations of triggered webjobs on a web app. |
| `az webapp webjob triggered list` | List all triggered webjobs hosted on a web app. |
| `az webapp webjob triggered log` | Get history of a specific triggered webjob hosted on a web app. |
| `az webapp webjob triggered remove` | Delete a specific triggered webjob hosted on a web app. |
| `az webapp webjob triggered run` | Run a specific triggered webjob hosted on a web app. |

[Az Cli Webjob Commands](https://learn.microsoft.com/en-us/cli/azure/webapp/webjob?view=azure-cli-latest)


## Helpful SSH Commands
To Enter an Instance SSH Emulated console:

[https://\<appname>.scm.azurewebsites.net/webssh/host](https://appname.scm.azurewebsites.net/webssh/host)


### Install and Manage Processes
```
    htop       Montior processes with a Text Based Gui
    ps aux     List Processes
    top        List and Monitor processes
```


### Follow the last 10 lines of a continuous log file.
```
    tail -f -s 1 -n 10 /data/jobs/continuous/<webjobname>/job_log.txt
```


### Edit and Read files from CLI
```
    nano <filename>            Edit the file with a CLI text editor (vim,nano.ect.)
    cat <filename>             Read the file to console
    echo "" >> <filename>      Append a file with the String
```