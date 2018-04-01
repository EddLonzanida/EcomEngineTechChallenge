export class EmailtemplateSearchRequest {
    constructor(public search = '',
        public page = 0,
        public desc = false,
        public field = 0) { }
}
