namespace ConferenceApp.Controllers {

    export class ConfEditController {
        public name: string;
        public startDate: Date;
        public endDate: Date;
        public street: string;
        public city: string;
        public state: string;
        public zip: string;
        public imageUrl: string;
        public confId: number;
        public addressId: number;

        public editedConf;

        public EditConference() {

            let editedConf = {
                name: this.name,
                startDate: this.startDate,
                endDate: this.endDate,
                imageUrl: this.imageUrl,
                street: this.street,
                city: this.city,
                state: this.state,
                zip: this.zip,
                addressId: this.addressId
            }

            console.log(JSON.stringify(editedConf));

            this.$http.post('/api/conferences/' + this.confId, JSON.stringify(editedConf))
                .then((response) => {
                    this.$state.go("confManage");
                    //console.log("successful post");
                });
        }
    

        public DeleteConference() {           
            this.$http.delete(`/api/conferences/${this.confId}`)
                .then((response) => {
                    this.$state.go("confManage");
                })
                .catch((response) => {
                    console.log(response.data);
                });

        }

        constructor(private $http: ng.IHttpService,
            private $state: ng.ui.IStateService,
            $stateParams: ng.ui.IStateParamsService) {
            //console.log($stateParams['id']);
            $http.get('/api/conferences/' + $stateParams['id'])
                .then((response) => {
                    this.editedConf = response.data;
                    console.log(this.editedConf);
                    this.name = this.editedConf.name;
                    this.startDate = new Date(this.editedConf.startDate);
                    this.endDate = new Date(this.editedConf.endDate);
                    this.street = this.editedConf.street;
                    this.city = this.editedConf.city;
                    this.state = this.editedConf.state;
                    this.zip = this.editedConf.zip;
                    this.confId = this.editedConf.id;
                    this.imageUrl = this.editedConf.imageUrl;
                    this.addressId = this.editedConf.addressId;
                })
                .catch((response) => {
                    console.log(response.data);
                });
            
        }
    }
}