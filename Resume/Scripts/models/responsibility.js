function Responsibility(responsibility) {
    var self = this;

    self.Id = ko.observable(responsibility.Id);
    self.Name = ko.observable(responsibility.Name);
    self.Description = ko.observable(responsibility.Description);
    self.PositionId = ko.observable(responsibility.PositionId);
    self.Percentage = ko.observable(responsibility.Percentage);
    self.dataify = ko.toJSON(self);
}