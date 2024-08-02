<template>
    <div class="container" :class="{'centered-container' : props.isCentered, 'default-container' : !props.isCentered}">
        <slot name="formInformation"></slot>
    </div>
</template>

<script lang="ts" setup>
/* eslint-disable */
const props = defineProps({
  isCentered: {
    type: Boolean,
    required: false,
    default: false,
  },
});
</script>

<style lang="scss" scoped>
@import "./FormContainer.scss";
</style>
