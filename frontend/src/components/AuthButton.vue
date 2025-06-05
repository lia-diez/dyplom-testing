<script setup lang="ts">

import {ref} from "vue";

const modalVisible = ref(false);

const formdata = ref({
  username: '',
  password: '',
  password2: ''
});

const loged_in = ref(true);

</script>

<template>
  <div v-if="loged_in">
    <Button @click="modalVisible = !modalVisible" rounded>Увійти</Button>
    <Dialog class="modal" v-model:visible="modalVisible" modal
            :draggable="false" :show-header="false" style="border-radius: 2rem;">

      <Tabs value="0" class="tabs">
        <TabList>
          <Tab value="0"><h3>Вхід</h3></Tab>
          <Tab value="1"><h3>Реєстрація</h3></Tab>
        </TabList>
        <TabPanels>
          <TabPanel value="0">
            <InputText type="text" placeholder="Ім'я користувача" class="input" v-model="formdata.username"/>
            <InputText type="password" placeholder="Пароль" class="input" v-model="formdata.password"/>
            <div class="right">
              <Button type="submit" severity="primary" label="Увійти" class="submit" rounded/>
            </div>

          </TabPanel>
          <TabPanel value="1">
            <InputText type="text" placeholder="Ім'я користувача" class="input" v-model="formdata.username"/>
            <InputText type="password" placeholder="Пароль" class="input" v-model="formdata.password"/>
            <InputText type="password" placeholder="Підтвердження пароля" class="input" v-model="formdata.password2"/>
            <div class="right">
              <Button type="submit" severity="primary" label="Реєстрація" class="submit" rounded/>
            </div>

          </TabPanel>
        </TabPanels>
      </Tabs>
    </Dialog>
  </div>
  <div v-else>
    <router-link to="/profile">test-user-1234</router-link>
  </div>
</template>

<style scoped>


.tabs {
  margin-top: 1rem;
  width: 20rem;
}

.input {
  width: 100%;
  margin-bottom: 1rem;
}

.right {
  display: flex;
  justify-content: flex-end;
}

</style>