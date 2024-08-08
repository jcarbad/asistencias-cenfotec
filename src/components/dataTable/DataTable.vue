<template>
  <div
    v-if="props.additionalInformation"
    class="additional-information-container"
  >
    <slot name="additionalInformation"></slot>
  </div>
  <div class="table-data-container">
    <table class="table table-striped" :aria-label="'Listado con ' + rowQuantity  + ' registros'">
      <thead class="table-header">
        <th v-for="item in props.tableHeaders" :key="item.id" scope="col">
          {{ item.value }}
        </th>
      </thead>
      <tbody>
        <slot name="dataRows"></slot>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts" setup>
import { IDataTableInfo } from "@/models/IDataTableInfo";
import { PropType } from "vue";

/* eslint-disable */
const props = defineProps({
  tableHeaders: {
    type: Array as PropType<Array<IDataTableInfo>>,
    required: false,
  },
  additionalInformation: {
    type: Boolean,
    default: false,
  },
  rowQuantity: {
    type: Number,
    default: 0
  }
});
</script>

<style lang="scss" scoped>
@import "./DataTable.scss";
</style>
