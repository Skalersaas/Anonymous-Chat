<script>

</script>

<template>
<div>
    <h2 class="text-2xl font-semibold text-gray-700 mb-4">Active Users</h2>
    <div class="overflow-x-auto bg-white shadow-md rounded-lg">
        <table class="min-w-full table-auto">
        <thead class="bg-gray-200 text-gray-600 uppercase text-sm">
            <tr>
            <th class="py-3 px-6 text-left">User ID</th>
            <th class="py-3 px-6 text-left">Pseudonym</th>
            <th class="py-3 px-6 text-left">Gender</th>
            <th class="py-3 px-6 text-left">Status</th>
            </tr>
        </thead>
        <tbody class="text-gray-600 text-sm">
            <tr class="border-b border-gray-200" v-for="user in users" :key="user.id">
            <td class="py-3 px-6">{{ user.id }}</td>
            <td class="py-3 px-6">{{ user.pseudonym }}</td>
            <td class="py-3 px-6">{{ user.gender == 1 ? "Male" :
                                     user.gender == 2 ? "Female" : "Other" }}</td>
            <td class="py-3 px-6">{{ user.status == 0 ? "Idle" :
                                     user.status == 1 ? "Waiting" : "Talking" }}</td>
            </tr>
        </tbody>
        </table>
    </div>
</div>
</template>
<script setup>
defineProps({
    users: Array
})
</script>