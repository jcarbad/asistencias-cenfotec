<template>
  <FormContainer>
    <template #formInformation>
      <fieldset>
        <h1>Recuperar Contraseña</h1>
        <TextField :textFieldType="'text'" :textFieldHeader="'Usuario'" />
        <MultiUseButton
          :textValue="'Regresar'"
          :buttonType="'link'"
          @click="backToLogin"
        />
        <MultiUseButton
          :textValue="'Enviar'"
          :buttonType="'primary'"
          @click="recoveryVerifier"
          data-targetdata-bs-target="#exampleModal"
        />
      </fieldset>
    </template>
  </FormContainer>
  <ConfirmModal @close="toggleModal" :modalActive="showModal">
    <div class="modal-content">
      <h1>This is a Modal Header</h1>
      <p>This is a modal message</p>
    </div>
  </ConfirmModal>
</template>

<script lang="ts" setup>
import { useRouter } from "vue-router";

import ConfirmModal from "@/components/confirmModal/ConfirmModal.vue";
import TextField from "@/components/textField/TextField.vue";
import FormContainer from "@/components/formContainer/FormContainer.vue";
import MultiUseButton from "@/components/multiUseButton/MultiUseButton.vue";
import { ref } from "vue";

const router = useRouter();
const showModal = ref(false);

const toggleModal = () => {
  showModal.value = !showModal.value;
};

function backToLogin() {
  router.push({
    name: "login"
  })
}

const recoveryVerifier = () => {
  toggleModal()
};
</script>

<style lang="scss">
@import url("RecoveryView.scss");
</style>
