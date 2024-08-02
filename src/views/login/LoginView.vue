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

const router = useRouter();
const userStore = useUserStore();

let usernameData = ref("");
let passwordData = ref("");

const recoveryEvent = () => {
  router.push({ name: "recovery" });
};

const loginVerifier = () => {
  userStore.setCurrentUser(usernameData.value);
  router.push({ name: "dashboard" });
};
</script>

<style lang="scss">
@import url("LoginView.scss");
</style>
