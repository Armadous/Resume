function Responsibility(responsibility) {
    var self = this;

    self.Name = ko.observable(responsibility.Name);
    self.Description = ko.observable(responsibility.Description);
    self.PositionId = ko.observable(responsibility.PositionId);
}