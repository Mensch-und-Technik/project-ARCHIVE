- install VS Code
- install Unity 2022.3.21f1
- setup WAMP (Windows Apache MySQL PHP)
    - (required) install Visual C++ Redistributable Packages
        download from here: https://github.com/abbodi1406/vcredist/releases
    - install WAMP
        download from here https://sourceforge.net/projects/wampserver/files/latest/download
    - setup SSL
        - follow this guide till step 4 https://puvox.software/blog/install-ssl-https-on-wamp-server/
            - add the following lines of code to httpd-ssl.conf as well:

            ´´´
            <Directory "${INSTALL_DIR}/www/">
                Options +Indexes +Includes +FollowSymLinks +MultiViews
                AllowOverride All
                Require all granted
            </Directory>
            ´´´

            - finish the guide above till the end
        - allow port 443 through firewall
            https://superuser.com/questions/432794/how-to-allow-remote-access-to-my-wamp-server
        - allow WAMP through firewall
            https://thewindowsclub.blog/de/how-to-allow-apps-through-firewall-on-windows-11/
    - !setup database
- test (remotely) https://192.168.ABC.DE:443
- useful references:
    https://blog.containerize.com/de/how-to-install-and-configure-wamp-server-on-windows/
    https://wampserver.aviatechno.net/
    https://stackoverflow.com/questions/24005828/how-to-enable-local-network-users-to-access-my-wamp-sites
    https://stackoverflow.com/questions/44716208/configure-ssl-in-apache-with-virtualhost
    https://infyom.com/blog/how-to-enable-localhost-https-ssl-on-wamp-server
    https://superuser.com/questions/432794/how-to-allow-remote-access-to-my-wamp-server
