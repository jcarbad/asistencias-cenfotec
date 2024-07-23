<template>
  <button
    :class="{
      'primary-button': buttonType === 'primary',
      'warning-button': buttonType === 'warning',
      'link-button': buttonType === 'link',
    }"
    @click="loginEvent"
  >
    {{ props.textValue }}
  </button>
</template>

<script lang="ts" setup>
// eslint-disable-next-line
const props = defineProps({
  buttonType: {
    type: String,
    required: true,
    default: "primary",
  },
  textValue: {
    type: String,
    required: true,
    default: "Button",
  },
});

// eslint-disable-next-line
const emit = defineEmits(["login"]);

const loginEvent = () => {
  //emit("login");
};
</script>

<style lang="scss" scoped>
@import "./MultiUseButton.scss";
</style>
