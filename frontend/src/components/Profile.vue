<script setup lang="ts">
const keys = [
  {key: 'QvsTvZ6KjbggsjWQgJjSuhKntqPp5NjH', expiration_date: '2024-12-31'},
  {key: 'P6aaKGC7qzXhSGQYTcxhLjf6fstQH9qq', expiration_date: '2025-01-15'},
  {key: 'erpyJhnnEs6SpGJFVqhGCn2xBzxWfeek', expiration_date: '2025-02-20'}
]
</script>

<template>
  <div class="container">
    <div class="main">
      <panel style="border-radius: 2rem; padding: 1rem;">
        <template #header>

          <div class="column">
            <h3>Профіль користувача</h3>
            <div class="header">
              <h2>test-user-1234</h2>
              <Button severity="secondary" icon="pi pi-sign-out" rounded></Button>
            </div>
          </div>
        </template>
        <div class="column">
          <div class="card">

            <DataTable :value="keys" tableStyle="min-width: 50rem">
              <template #header>
                <div class="tbl-header">
                  <h2>Ключі доступу</h2>
                  <Button icon="pi pi-plus"  severity="primary" rounded label="Додати"></Button>
                </div>
              </template>
              <Column field="key" header="Ключ"></Column>
              <Column field="expiration_date" header="Дійсний до"></Column>

              <Column class="w-24 !text-end">
                <template #body="">
                  <div class="header">
                    <Button class="tbl-btn" icon="pi pi-clone" @click="" severity="secondary"
                            rounded></Button>
                    <Button class="tbl-btn" icon="pi pi-trash" @click="" severity="contrast"
                            rounded></Button>
                  </div>
                </template>
              </Column>
            </DataTable>
          </div>
        </div>


      </panel>
    </div>
  </div>
</template>

<style scoped>
.container {
  display: grid;
  grid-template-columns: 1fr 3fr 1fr;
}

.main {
  grid-column: 2;
  margin-top: 1rem;
  width: 60vw;
}

.column {
  display: flex;
  flex-direction: column;
}

h3 {
  font-weight: normal;
}

.header {
  display: flex;
  flex-direction: row;
  align-items: center;
  gap: 0.5rem;
  width: 100%;
}

.tbl-header {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
}

</style>