<template>
<div>
    <h2 class="text-2xl font-semibold text-gray-700 mb-4">Pairs</h2>
    <div class="overflow-x-auto bg-white shadow-md rounded-lg">
    <table class="min-w-full table-auto">
        <thead class="bg-gray-200 text-gray-600 uppercase text-sm">
        <tr>
            <th class="py-3 px-6 text-left">User 1</th>
            <th class="py-3 px-6 text-left">User 2</th>
        </tr>
        </thead>
        <tbody class="text-gray-600 text-sm">
        <tr class="border-b border-gray-200" v-for="pair in pairs" :key="pair.id">
            <td class="py-3 px-6">{{ pair.item1 }}</td>
            <td class="py-3 px-6">{{ pair.item2 }}</td>
        </tr>
        </tbody>
    </table>
    </div>
</div>
</template>
<script setup>
defineProps({
    pairs: Array
})
</script>