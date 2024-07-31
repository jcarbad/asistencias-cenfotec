<template>
  <table class="table table-striped">
    <thead class="table-header">
      <th v-for="(item) in props.tableHeaders" :key="item.id" scope="col">
        {{ item.value }}
      </th>
    </thead>
    <tbody>
      <slot name="dataRows"></slot>
    </tbody>
  </table>
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
});

</script>

<style lang="scss" scoped>
@import "./DataTable.scss";
</style>
