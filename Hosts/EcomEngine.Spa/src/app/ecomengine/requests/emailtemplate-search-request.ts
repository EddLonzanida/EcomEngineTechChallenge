export class EmailtemplateSearchRequest {
    constructor(public search = "",
        public page = 0,
        public isDescending = false,
        public sortColumn = '') { }
}
