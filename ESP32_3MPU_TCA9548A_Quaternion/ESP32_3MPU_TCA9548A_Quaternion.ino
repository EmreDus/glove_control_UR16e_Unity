#include <I2Cdev.h>
#include <MPU6050_6Axis_MotionApps_V6_12.h>
#include <Wire.h>

#include <WiFi.h>

const char* ssid = "Emre";       // Replace with your WiFi network name
const char* password = "93259325"; // Replace with your WiFi network password

IPAddress staticIP(192, 168, 1, 155); // Replace with your desired static IP address
IPAddress gateway(192, 168, 1, 1);    // Replace with your gateway IP address
IPAddress subnet(255, 255, 255, 0);   // Replace with your subnet mask

WiFiServer server(80);

// initialize MPUs
MPU6050 mpu1(0x68);
MPU6050 mpu2(0x68);
MPU6050 mpu3(0x68);

void tcaselect(uint8_t bus) { 
  Wire.beginTransmission(0x70);
  Wire.write(1 << bus);
  Wire.endTransmission();  
}

void initWiFi() {
  WiFi.mode(WIFI_STA);
  WiFi.begin(ssid, password);
  Serial.print("Connecting to WiFi ..");
  while (WiFi.status() != WL_CONNECTED) {
    Serial.print('.');
    delay(1000);
  }
  // Set static IP address
  WiFi.config(staticIP, gateway, subnet);
  Serial.println(WiFi.localIP());
  //Start the TCP server
  server.begin();
  Serial.printf("Web server started, open %s in a web browser\n", WiFi.localIP().toString().c_str());
}

uint8_t error_code = 0U;      // return status after each device operation (0 = success, !0 = error)

void setup() {
  Wire.begin();
  Serial.begin(115200);

  // ############################# Initialize MPU1 ########################################
  tcaselect(7);
  mpu1.initialize();
  error_code = mpu1.dmpInitialize(); 
  // 1 = initial memory load failed
  // 2 = DMP configuration updates failed
  // (if it's going to break, usually the code will be 1)
  if (error_code == 1U) {
    Serial.print("{\"key\": \"/log\", \"value\": \"device 1 initialization failed: initial memory load failed.\", \"level\": \"ERROR\"}\n");
    while (1) {}
  }
  if (error_code == 2U) {
    Serial.print("{\"key\": \"/log\", \"value\": \"device 1 initialization failed: DMP configuration updates failed.\", \"level\": \"ERROR\"}\n");
    while (1) {}
  }

  // verify connection
  if (!mpu1.testConnection()) {
    Serial.print("{\"key\": \"/log\", \"value\": \"device 1 connection failed.\", \"level\": \"ERROR\"}\n"); 
  }

  // Offsets
  mpu1.setXGyroOffset(0);
  mpu1.setYGyroOffset(0);
  mpu1.setZGyroOffset(0);
  mpu1.setXAccelOffset(0);
  mpu1.setYAccelOffset(0);
  mpu1.setZAccelOffset(0);

  // Calibration
  mpu1.CalibrateAccel(6);
  mpu1.CalibrateGyro(6);

  // turn on the DMP
  mpu1.setDMPEnabled(true);
  Serial.print("\n");
  // ######################################################################################

  delay(100);

  // ############################# Initialize MPU2 ########################################
  tcaselect(5);
  mpu2.initialize();
  error_code = mpu2.dmpInitialize();
  // 1 = initial memory load failed
  // 2 = DMP configuration updates failed
  // (if it's going to break, usually the code will be 1)
  if (error_code == 1U) {
    Serial.print("{\"key\": \"/log\", \"value\": \"device 2 initialization failed: initial memory load failed.\", \"level\": \"ERROR\"}\n");
    while (1) {}
  }
  if (error_code == 2U) {
    Serial.print("{\"key\": \"/log\", \"value\": \"device 2 initialization failed: DMP configuration updates failed.\", \"level\": \"ERROR\"}\n");
    while (1) {}
  }

  // verify connection
  if (!mpu2.testConnection()) {
    Serial.print("{\"key\": \"/log\", \"value\": \"device 2 connection failed.\", \"level\": \"ERROR\"}\n");
  }

  // Offsets
  mpu2.setXGyroOffset(0);
  mpu2.setYGyroOffset(0);
  mpu2.setZGyroOffset(0);
  mpu2.setXAccelOffset(0);
  mpu2.setYAccelOffset(0);
  mpu2.setZAccelOffset(0);
  
  // Calibration
  mpu2.CalibrateAccel(6);
  mpu2.CalibrateGyro(6);

  // turn on the DMP
  mpu2.setDMPEnabled(true);
  Serial.print("\n");
  // ######################################################################################

  delay(100);

  // ############################# Initialize MPU3 ########################################
  tcaselect(1);
  mpu3.initialize();
  error_code = mpu3.dmpInitialize();
  // 1 = initial memory load failed
  // 2 = DMP configuration updates failed
  // (if it's going to break, usually the code will be 1)
  if (error_code == 1U) {
    Serial.print("{\"key\": \"/log\", \"value\": \"device 3 initialization failed: initial memory load failed.\", \"level\": \"ERROR\"}\n");
    while (1) {}
  }
  if (error_code == 2U) {
    Serial.print("{\"key\": \"/log\", \"value\": \"device 3 initialization failed: DMP configuration updates failed.\", \"level\": \"ERROR\"}\n");
    while (1) {}
  }

  // verify connection
  if (!mpu3.testConnection()) {
    Serial.print("{\"key\": \"/log\", \"value\": \"device 3 connection failed.\", \"level\": \"ERROR\"}\n");
  }

  // Offsets
  mpu3.setXGyroOffset(0);
  mpu3.setYGyroOffset(0);
  mpu3.setZGyroOffset(0);
  mpu3.setXAccelOffset(0);
  mpu3.setYAccelOffset(0);
  mpu3.setZAccelOffset(0);
  
  // Calibration
  mpu3.CalibrateAccel(6);
  mpu3.CalibrateGyro(6);

  // turn on the DMP
  mpu3.setDMPEnabled(true);
  Serial.print("\n");
  // ##################################################################################

  delay(100);
  initWiFi(); // initialize WiFi

}

void loop() {
  //Listen to connecting clients
  WiFiClient client = server.available();

  if(client)
  {
    Serial.println("Client Connected");
    while(client.connected())
    {
      tcaselect(7);  // ----------------Print MPU1 Quaternions----------------
      uint8_t fifo_buffer1[64]; // FIFO storage buffer
      if (!mpu1.dmpGetCurrentFIFOPacket(fifo_buffer1)) {
        //return;
      } 
      Quaternion q1;           // [w, x, y, z]
      mpu1.dmpGetQuaternion(&q1, fifo_buffer1);
      
      client.print(q1.w); client.print("/");
      client.print(q1.x); client.print("/");
      client.print(q1.y); client.print("/");
      client.print(q1.z); client.print("/");
      // --------------------------------------------------------------------

      tcaselect(5); // ----------------Print MPU2 Quaternions----------------
      uint8_t fifo_buffer2[64]; // FIFO storage buffer
      if (!mpu2.dmpGetCurrentFIFOPacket(fifo_buffer2)) {
        //return;
      }   
      Quaternion q2;           // [w, x, y, z]  
      mpu2.dmpGetQuaternion(&q2, fifo_buffer2);
                
      client.print(q2.w); client.print("/");
      client.print(q2.x); client.print("/");
      client.print(q2.y); client.print("/");
      client.print(q2.z); client.print("/");
      
      // --------------------------------------------------------------------

      tcaselect(1); // ----------------Print MPU3 Quaternions----------------
      uint8_t fifo_buffer3[64]; // FIFO storage buffer 
      if (!mpu3.dmpGetCurrentFIFOPacket(fifo_buffer3)) {
        //return;
      }
      Quaternion q3;           // [w, x, y, z]  
      mpu3.dmpGetQuaternion(&q3, fifo_buffer3);
          
      client.print(q3.w); client.print("/");
      client.print(q3.x); client.print("/");
      client.print(q3.y); client.print("/");
      client.print(q3.z); client.print("/");
      
      // --------------------------------------------------------------------

      // ----------------Fingers---------------
      int serce = analogRead(34);
      int yuzuk = analogRead(35);
      int orta = analogRead(32);
      int isaret = analogRead(33);
      int bas = analogRead(25);
      
      serce = map(serce, 0, 4095, 100, 0);
      yuzuk = map(yuzuk, 0, 4095, 50, 0);
      orta = map(orta, 0, 4095, 50, 0);
      isaret = map(isaret, 0, 4095, 50, 0);
      bas = map(bas, 0, 4095, 50, 0);
      
      client.print(serce); client.print("/");
      client.print(yuzuk); client.print("/");
      client.print(orta); client.print("/");
      client.print(isaret); client.print("/");
      client.print(bas); client.print("\r");

      // --------------------------------------------------------------------
      delay(20);
    }
    delay(20);
    // close the connection:
    client.stop();
    Serial.println("Client Disconnected.");
  }
}