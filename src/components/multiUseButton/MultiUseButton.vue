<template>
  <input 
    type="button"
    :value="props.textValue"
    :id="valueForId"
    :class="{
      'primary-button': buttonType === 'primary',
      'secondary-button': buttonType === 'secondary',
      'warning-button': buttonType === 'warning',
      'link-button': buttonType === 'link',
    }"
  />
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
  idValue: {
    type: String,
    required: false,
  },
});

const valueForId = props.idValue ? props.idValue : props.textValue;

// eslint-disable-next-line
const emit = defineEmits(["clickEvent"]);

</script>

<style lang="scss" scoped>
@import "./MultiUseButton.scss";
</style>
