Position = Backbone.Model.extend();

Positions = Backbone.Collection.extend({
    model: Position,
    url: '/api/position',

    initialize: function () {
        this.fetch({
            success: this.fetchSuccess,
            error: this.fetchError
        });
    },

    fetchSuccess: function (collection, response) {
        console.log('Collection fetch success', response);
        console.log('Collection models: ', collection.models);
    },

    fetchError: function (collection, response) {
        throw new Error("Fetch error " + response);
    }
});