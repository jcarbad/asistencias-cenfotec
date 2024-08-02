<template>
  <div style="margin-top: 10px; width: 300px;">
    <label class="textfield-label-standard">
      {{ props.textFieldHeader }}
      <input
        :type="props.textFieldType"
        class="textfield-standard"
      />
    </label>
  </div>
</template>

<script lang="ts" setup>
// eslint-disable-next-line
const props = defineProps({
  textFieldType: {
    type: String,
    required: true,
    default: "text",
  },
  textFieldHeader: {
    type: String,
    required: true,
  },
});
</script>

<style lang="scss" scoped>
@import "./TextField.scss";
</style>
