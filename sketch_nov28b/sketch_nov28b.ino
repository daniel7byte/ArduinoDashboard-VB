int pirState = LOW;           // de inicio no hay movimiento
int val = LOW;                  // estado del pin

void setup() {
  Serial.begin(9600);
  pinMode(13, OUTPUT);
  pinMode(10, INPUT);

  Serial.println("p");
}

void loop() {
  if(Serial.available()>0){
    String txt = Serial.readString();
    if(txt == "13 1"){
        digitalWrite(13, 1);
        Serial.println("PIN 13: ON");
    }
    if(txt == "13 0"){
        digitalWrite(13, 0);
        Serial.println("PIN 13: OFF");
    }
  }

  // SENSOR PIR

  val = digitalRead(10);
  if (val == HIGH) { 
    //si está activado
    if (pirState == LOW) {
      //si previamente estaba apagado
      Serial.println("P");
      pirState = HIGH;
    }
  } else {
    //si esta desactivado
    if (pirState == HIGH) {
      //si previamente estaba encendido
      Serial.println("p");
      pirState = LOW;
    }
  }
}
