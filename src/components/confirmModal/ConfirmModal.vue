<template>
  <transition name="modal-animation">
    <div v-show="props.modalActive" class="modal">
      <transition name="modal-animation-inner">
        <div v-show="modalActive" class="modal-inner">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="24"
            height="24"
            fill="currentColor"
            class="bi bi-x-lg"
            viewBox="0 0 24 24"
            @click="close"
          >
            <path
              d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z"
            />
          </svg>
          <!-- Modal Content -->
          <slot name="content" />
          <MultiUseButton v-if="modalType == 'information'" :textValue="'Aceptar'" :buttonType="'primary'" @click="close" />
          <MultiUseButton v-if="modalType == 'confirmation'" :textValue="'No'" :buttonType="'secondary'" @click="noOptionSelected" />
          <MultiUseButton v-if="modalType == 'confirmation'" :textValue="'Sí'" :buttonType="'primary'" @click="yesOptionSelected" />
        </div>
      </transition>
    </div>
  </transition>
</template>

<script lang="ts" setup>
import MultiUseButton from '../multiUseButton/MultiUseButton.vue';
/* eslint-disable */
const emit = defineEmits(["closeModal", "noOptionSelected", "yesOptionSelected"]);

const props = defineProps({
  modalActive: {
    type: Boolean,
    default: false,
  },
  modalType: {
    type: String,
    default: "confirmation"
  }
});

const close = () => {
  emit("closeModal");
};

const noOptionSelected = () => {
  emit("noOptionSelected");
}

const yesOptionSelected = () => {
  emit("yesOptionSelected");
}
</script>

<style lang="scss" scoped>
.modal-animation-enter-active,
.modal-animation-leave-active {
  transition: opacity 0.3s cubic-bezier(0.52, 0.02, 0.19, 1.02);
}

.modal-animation-enter-from,
.modal-animation-leave-to {
  opacity: 0;
}

.modal-animation-inner-enter-active {
  transition: all 0.3s cubic-bezier(0.52, 0.02, 0.19, 1.02) 0.15s;
}

.modal-animation-inner-leave-active {
  transition: all 0.3s cubic-bezier(0.52, 0.02, 0.19, 1.02);
}

.modal-animation-inner-enter-from {
  opacity: 0;
  transform: scale(0.8);
}

.modal-animation-inner-leave-to {
  transform: scale(0.8);
}

.modal {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  width: 100vw;
  position: fixed;
  top: 0;
  left: 0;
  background-color: rgba(0, 0, 0, 0.7);

  .modal-inner {
    position: relative;
    max-width: 640px;
    width: 80%;
    box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1),
      0 2px 4px -1px rgba(0, 0, 0, 0.06);
    background-color: #fff;
    padding: 20px 16px;

    svg {
      float: right;
    }

    i {
      position: absolute;
      top: 15px;
      right: 15px;
      font-size: 20px;
      cursor: pointer;

      &:hover {
        color: crimson;
      }
    }
  }
}
</style>
