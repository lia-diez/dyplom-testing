<script setup lang="ts">

import AuthButton from "./components/AuthButton.vue";

const changeTheme = () => {
  document.documentElement.classList.toggle('app-dark');
};

</script>

<template>
  <div class="app-container">
    <Toolbar style="border-radius: 2rem;">

      <template #start>
        <div class="header-buttons">
          <Button severity="contrast" @click="changeTheme" rounded>
            <template #icon>
              <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none"
                   stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"
                   class="feather feather-moon">
                <path d="M21 12.79A9 9 0 1 1 11.21 3 7 7 0 0 0 21 12.79z"></path>
              </svg>
            </template>
          </Button>
          <Button as="a" label="GitHub" severity="secondary" icon="pi pi-github" rounded
                  href="https://github.com/jiangtian616/JHenTai" target="_blank" rel="noopener"/>

        </div>
      </template>

      <template #center>
        <router-link to="/">
          <h1>TerraLingua</h1>
        </router-link>

        <Badge class="badge">v1</Badge>
      </template>
      <template #end>
        <AuthButton></AuthButton>

      </template>
    </Toolbar>
    <RouterView></RouterView>
  </div>
</template>

<style scoped>
.badge {
  margin-top: -1rem;
}

.app-container {
  display: flex;
  flex-direction: column;
  height: 100%;
}

.header-buttons {
  display: flex;
  align-items: center;
  gap: 1rem;
}
</style>


