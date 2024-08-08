<template>
  <div style="margin-top: 10px; width: 300px;">
    <label class="textfield-label-standard">
      {{ props.textFieldHeader }}
      <input
        :type="props.textFieldType"
        :value="modelValue"
        :aria-label="'Ingrese el valor del ' + props.textFieldHeader"
        @input="handleInput($event)"
        class="textfield-standard"
      />
    </label>
  </div>
</template>

<script lang="ts" setup>
import { defineEmits } from 'vue';
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
  modelValue: {
    type: String,
    required: true,
    default: ""
  }
});

const emit = defineEmits(['update:modelValue'])

const handleInput = ($event: any) => {
  emit('update:modelValue', $event.target.value)
}
</script>

<style lang="scss" scoped>
@import "./TextField.scss";
</style>
