<template>
  <FormContainer>
    <template #formInformation>
      <fieldset>
        <h1>Login</h1>
        <TextField :textFieldType="'text'" :textFieldHeader="'Username'"/>
        <TextField :textFieldType="'password'" :textFieldHeader="'Password'"/>
        <MultiUseButton :text-value="'Login'" @login="loginVerifier" />
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

const loginVerifier = () => {
  router.push({name: "dashboard"});
}
</script>

<style lang="scss">
@import url("LoginView.scss");
</style>
