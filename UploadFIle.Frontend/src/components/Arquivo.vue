<template>
  <div class="page-container">
    <md-app md-mode="reveal">
      <md-app-toolbar class="md-primary">
        <md-button class="md-icon-button" @click="menuVisible = !menuVisible">
          <md-icon>menu</md-icon>
        </md-button>
        <span class="md-title">UploadFile.Client</span>
      </md-app-toolbar>

      <md-app-drawer :md-active.sync="menuVisible">
        <md-toolbar class="md-transparent" md-elevation="0">Navegação</md-toolbar>

        <md-list>
          <md-list-item>
            <md-button class="md-accent md-raised" @click="logout()" >
              <a style="text-decoration:none; color: white">
                <span class="md-list-item-text">Logout</span>
              </a>
            </md-button>
          </md-list-item>

        </md-list>
      </md-app-drawer>

      <md-app-content>
         <md-table v-model="searched" md-sort="name" md-sort-order="asc" md-card md-fixed-header>
      <md-table-toolbar>
        <div class="md-toolbar-section-start">
          <h1 class="md-title">File</h1>
        </div>

        <md-button v-if="!(files.lenght === 0)"  style="max-width:50px" class="md-primary md-raised md-toolbar-section-end" >
          <router-link style="text-decoration:none; color: white" to="/NewFile">
            <p>ADD</p>
          </router-link>
        </md-button>
        <md-field md-clearable class="md-toolbar-section-end">
          <md-input placeholder="Buscar por nome de arquivo..." v-model="search" @input="searchOnTable" />
        </md-field>
      </md-table-toolbar>

      <md-table-empty-state
        md-label="Nenhum arquivo encontrado">
        <md-button class="md-primary md-raised" >
          <router-link style="text-decoration:none; color: white" to="/NewFile">
            <p>Add novo arquivo</p>
          </router-link>
        </md-button>
      </md-table-empty-state>

      <md-table-row style="text-align: left;" slot="md-table-row" slot-scope="{ item }">
        <md-table-cell md-label="ID" md-sort-by="id" md-numeric>{{ item.id }}</md-table-cell>
        <md-table-cell  md-label="Nome do Arquivo" md-sort-by="name">{{ item.fileName }}</md-table-cell>
        <md-table-cell md-label="Operações" >
          <md-button class="md-primary md-raised" @click="download(item)" >
          <a style="text-decoration:none; color: white">
            <p>Download</p>
          </a>
          </md-button>
          <md-button class="md-accent md-raised" @click="remover(item)" >
          <a style="text-decoration:none; color: white">
            <p>Remover</p>
          </a>
          </md-button>
        </md-table-cell>
      </md-table-row>
    </md-table>
      </md-app-content>
    </md-app>
  </div>
</template>

<style lang="scss" scoped>
  .md-app {
    border: 1px solid rgba(#000, .12);
  }

  .md-drawer {
    width: 230px;
    max-width: calc(100vw - 125px);
  }

  .md-field {
    max-width: 300px;
  }
</style>

<script>

 const toLower = text => {
    return text.toString().toLowerCase()
  }

  const searchByName = (items, term) => {
    if (term) {
      return items.filter(item => toLower(item.fileName).includes(toLower(term)))
    }

    return items
  }

export default {
  name: 'DashboardFile',
  data: () => ({
    menuVisible: false,
    search: null,
      searched: [],
      files: []
  }),
  methods: {
      download(item){

        this.$axios({
          url: 'http://localhost:5000/api/Document/download/'+ item.id,
          method: 'GET',
          responseType: 'blob',
        }).then((response) => {
          const url = window.URL.createObjectURL(new Blob([response.data]));
          const link = document.createElement('a');
          link.href = url;
          link.setAttribute('download', ''); 
          document.body.appendChild(link);
          link.click();
        });
      },
      remover(item){
        this.$axios(
          {
            url: 'http://localhost:5000/api/Document/' + item.id,
            method : 'DELETE'
        })
        .then( reponse =>{
          reponse;
          this.atualizar();
        })
        .catch( err =>{
          alert('Erro ao remover o arquivo');
          console.log(err)
        });
      },
      searchOnTable () {
        
        this.searched = searchByName(this.files, this.search)

      },
      logout(){
        this.$auth = ''
            localStorage.removeItem('token');
            this.$idUser = ''
            localStorage.idUser = null
            this.$nameUser = ''
            localStorage.userName = null
            this.$router.push('/');
      },
      atualizar(){
        this.$axios.get(
          'http://localhost:5000/api/Document/by-user'
        ).then(
          response =>{
            this.files = response.data
            this.searched = this.files;
            console.log(response.data)
          }
        ).catch(
          err => {
            
            console.log(err);
          }
        )
      }
    },
    created () {
      if(!localStorage.token || localStorage.token === null){
        this.$router.push('/');
        
        return;
      }

      this.atualizar();

    }
}
</script>
