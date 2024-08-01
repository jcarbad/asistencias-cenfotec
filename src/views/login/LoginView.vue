<template>
  <FormContainer>
    <template #formInformation>
      <fieldset>
        <h1>Ingresar</h1>
        <TextField :textFieldType="'text'" :textFieldHeader="'Usuario'"/>
        <TextField :textFieldType="'password'" :textFieldHeader="'Contraseña'"/>
        <MultiUseButton :textValue="'Recuperar Contraseña'" :buttonType="'link'" @click="recoveryEvent"/>
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

const router = useRouter();

const recoveryEvent = () => {
  router.push({name: "recovery"});
}

const loginVerifier = () => {
  router.push({name: "dashboard"});
}
</script>

<style lang="scss">
@import url("LoginView.scss");
</style>
