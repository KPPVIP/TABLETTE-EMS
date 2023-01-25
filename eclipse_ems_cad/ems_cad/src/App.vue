<template>
  <div id="app">
    <MedicalCad :playerData="playerData" v-if="showPage == 'MedicalCad'" />
  </div>
</template>

<script>
import MedicalCad from "./components/cad/MedicalCad";

export default {
  name: "App",
  components: {
    MedicalCad,
  },
  data() {
    return {
      showPage: "",
      characterList: [],
      playerData: {
        id: 1,
        phone: "8004455",
        name: "Javier Bennett",
        age: "33",
        roles: "ambulance",
        money: 4500099,
        policeData: {
          rank: 3,
          callsign: "MED-1",
          policeId: 5550000,
          onDuty: 1,
        },
        medicData: {
          rank: 3,
          callsign: "MED-1",
          medicId: 444,
          onDuty: 1,
        },
      },
    };
  },
  methods: {},
  computed: {},
  mounted() {
    window.addEventListener(
      "message",
      (event) => {
        if (event.data.type == "UpdatePlayerData") {
          this.playerData.phone = event.data.phone;
          this.playerData.name = event.data.name;
          this.playerData.roles = event.data.job;
          this.playerData.medicData.medicId = event.data.medicId;
          this.playerData.medicData.callsign = event.data.callsign;
          this.playerData.medicData.rank = event.data.rank;
          console.log(JSON.stringify(this.playerData))
        } else if (event.data.type == "OpenMedicalCad") {
          this.showPage = "MedicalCad";
        } else if (event.data.type == "CloseMedicalCad") {
          this.showPage = "";
        }
      },

      false
    );
  },
};
</script>

<style>
#app {
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}
@font-face {
  font-family: "weathericons";
  src: url("assets/font/weathericons-regular-webfont.eot");
  src: url("assets/font/weathericons-regular-webfont.eot?#iefix")
      format("embedded-opentype"),
    url("assets/font/weathericons-regular-webfont.woff2") format("woff2"),
    url("assets/font/weathericons-regular-webfont.woff") format("woff"),
    url("assets/font/weathericons-regular-webfont.ttf") format("truetype"),
    url("assets/font/weathericons-regular-webfont.svg#weather_iconsregular")
      format("svg");
  font-weight: normal;
  font-style: normal;
}
</style>
