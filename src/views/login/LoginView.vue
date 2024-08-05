<template>
  <FormContainer isCentered>
    <template #formInformation>
      <fieldset>
        <h1>Ingresar</h1>
        <TextField
          :textFieldType="'text'"
          :textFieldHeader="'Usuario'"
          v-model="usernameData"
        />
        <TextField
          :textFieldType="'password'"
          :textFieldHeader="'Contraseña'"
          v-model="passwordData"
        />
        <MultiUseButton
          :textValue="'Recuperar Contraseña'"
          :buttonType="'link'"
          @click="recoveryEvent"
        />
        <MultiUseButton :textValue="'Ingresar'" @click="loginVerifier" />
      </fieldset>
    </template>
  </FormContainer>
</template>

<script lang="ts" setup>
import { useRouter } from "vue-router";
import TextField from "@/components/textField/TextField.vue";
import FormContainer from "@/components/formContainer/FormContainer.vue";
import MultiUseButton from "@/components/multiUseButton/MultiUseButton.vue";
import { ref } from "vue";
import { useUserStore } from "@/store/useUserStore";
import axios from "axios";

const router = useRouter();
const userStore = useUserStore();

let usernameData = ref("");
let passwordData = ref("");

const recoveryEvent = () => {
  router.push({ name: "recovery" });
};

const loginVerifier = async () => {

  const data = JSON.stringify({
    username: usernameData.value,
    email: usernameData.value,
    password: passwordData.value,
  });

  await axios
    .post(
      "http://asistencias-api.us-east-1.elasticbeanstalk.com/api/Auth/login",
      data,
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    )
    .then((response) => {
      userStore.setCurrentUser(usernameData.value);
      let token = response.data.token.result;
      userStore.setAccessToken(token);
      router.push({ name: "dashboard" });
    })
    .catch((error) => {
      console.dir(error);
    });
};
</script>

<style lang="scss">
@import url("LoginView.scss");
</style>
