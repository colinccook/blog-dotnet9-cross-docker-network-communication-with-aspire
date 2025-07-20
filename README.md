# Cross Docker network communication with Aspire

My workplace has a docker-compose file with its own network and dozens of services. It represents the fundamental behaviour of our distributed application, and I can't change it.

I wanted to experiment with Aspire at work to host our team's infrastructure and I wondered if it was possible for them to communicate in the way I needed.

In short, it is.

This prototype demonstrates how an Aspire run service, or Docker container, can hit a Docker container run in parallel with docker-compose.

## Diagram

<img width="3587" height="1382" alt="image" src="https://github.com/user-attachments/assets/9c3f46b3-d39f-4d44-86a2-164819dfdb90" />

### Step 1: The consumer hits the web project `ColinCCook.AspireRunService`

Step 1a: The consumer can either call `/direct-request`, which is intended for the web project to try to call `http://barservice.colin` directly.

Step 1b: Or it can call `/via-container`, which calls a `mockservice` container for it to proxy to `http://barservice.colin` instead.

### Step 2: Resolving `http://barservice.colin`

The hosts file of the development environment points `http://barservice.com` to itself.

### Step 3: NGINX proxies calls to `http://barservice.com` to a another mockserver container

This NGINX container has been run via the `docker-compose.yaml` file, and shares a network with another mockserver container.

### Step 4: MockServer returns a Created/201 or Accepted/202, depending on the url.

If called with `/foo` it returns a 201 (if the web service hit it directly). 

If called with `/bar` it returns a 202 (via the proxy container in Aspire)

## Running the prototype yourself

There are integration tests that confirm that Created/201 and Accepted/202 are correctly returned by the web service.

In order for them to be run successfully, or to run Aspire yourself, follow these steps:

- Make sure your hosts file is set up to route requests for `barservice.colin`, see below for instructions
- Docker compose up the `/docker-compose/docker-compose.yaml` file
- Run the Aspire orchestration  `ColinCCook.AppHost.AppHost`, or, build and run the unit tests. 

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
