function Responsibility(responsibility) {
    this.Name = ko.observable(responsibility.Name);
    this.Description = ko.observable(responsibility.Description);
    this.PositionId = ko.observable(responsibility.PositionId);
}