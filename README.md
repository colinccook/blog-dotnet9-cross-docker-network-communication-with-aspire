# Cross Docker network communication with Aspire

My workplace has a docker-compose file with its own network and dozens of services. It represents the fundamental behaviour of our distributed application, and I can't change it.

I wanted to experiment with Aspire at work to host our team's infrastructure and I wondered if it was possible for them to communicate in the way I needed.

In short, it is.

This prototype demonstrates how an Aspire run service, or Docker container, can hit a Docker container run in parallel with docker-compose.

## Diagram

[Placeholder diagram]

## Getting Started

### Running the prototype

- Make sure your hosts file is set up to route requests for "barservice.colin", see below for instructions
- Run docker-compose up /docker-compose/docker-compose.yaml
- Run the Aspire hosted ColinCCook.AppHost.AppHost. 

### Observations

## Setting hosts file macOS

1. Open Terminal
2. Edit the hosts file using nano (recommended for beginners):
   ```bash
   sudo nano /etc/hosts
   ```

3. Add this line at the end of the file:
   ```
   127.0.0.1    barservice.colin
   ```

4. Save and exit:
   - Press `Ctrl+X`, then `Y`, then `Enter`

5. Flush DNS cache:
   ```bash
   sudo dscacheutil -flushcache
   sudo killall -HUP mDNSResponder
   ```

6. Verify the entry was added. It should output the line you've just added:
   ```bash
   cat /etc/hosts | grep barservice.colin
   ```

## Setting hosts file Windows

1. Open Command Prompt as Administrator:
   - Press `Win + X` and select "Command Prompt (Admin)" or "Windows PowerShell (Admin)"
   - Or search for "cmd" in Start menu, right-click and select "Run as administrator"

2. Edit the hosts file using notepad:
   ```cmd
   notepad C:\Windows\System32\drivers\etc\hosts
   ```

3. Add this line at the end of the file:
   ```
   127.0.0.1    barservice.colin
   ```

4. Save and exit:
   - Press `Ctrl+S` to save
   - Close Notepad

5. Flush DNS cache:
   ```cmd
   ipconfig /flushdns
   ```

6. Verify the entry was added:
   ```cmd
   findstr "barservice.colin" C:\Windows\System32\drivers\etc\hosts
   ```
