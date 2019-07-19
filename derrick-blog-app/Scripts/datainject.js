//This script will be used to inject data into views
//using the Vue.js framework
//purpose: keep a modular, easy to maintain structure

var titleLabel = new Vue({
        el: '#title-label',
        data: {
        message: @Html.DisplayNameFor(model => model.Title)
    }
})

var titleContent = new Vue({
    el: '#title-content',
        data: {
        message: @Html.DisplayFor(model => model.Title)
    }
})

var abstractLabel = new Vue({
    el: '#abstract-label',
        data: {
        message: @Html.DisplayNameFor(model => model.Abstract)
    }
})

var abstractContent = new Vue({
    el: '#abstract-content',
        data: {
        message: @Html.DisplayFor(model => model.Abstract)
    }
})

var slugLabel = new Vue({
    el: '#slug-label',
        data: {
        message: @Html.DisplayNameFor(model => model.Slug)
    }
})

var slugContent = new Vue({
    el: '#slug-content',
        data: {
        message: @Html.DisplayFor(model => model.Slug)
    }
})

var bodyLabel = new Vue({
    el: '#body-label',
        data: {
        message: @Html.Raw(Model.Body)
    }
})

var bodyContent = new Vue({
    el: '#body-content',
        data: {
        message: @Html.Raw(Model.Body)
    }
})

var mediaLabel = new Vue({
    el: '#media-label',
        data: {
        message: @Html.DisplayNameFor(model => model.MediaUrl)
    }
})

var mediaContent = new Vue({
    el: '#media-content',
        data: {
        message: @Html.DisplayFor(model => model.MediaUrl)
    }
})

var publishedLabel = new Vue({
    el: '#published-label',
        data: {
        message: @Html.DisplayNameFor(model => model.Published)
    }
})

var publishedContent = new Vue({
    el: '#published-content',
        data: {
        message: @Html.DisplayFor(model => model.Published)
    }
})

var createDateLabel = new Vue({
    el: '#create-label',
        data: {
        message: @Html.DisplayNameFor(model => model.Created)
    }
})

var createDateContent = new Vue({
    el: '#create-content',
        data: {
        message: @Html.DisplayFor(model => model.Created)
    }
})

var updateDateLabel = new Vue({
    el: '#update-label',
        data: {
        message: @Html.DisplayNameFor(model => model.Updated)
    }
})

var updateDateContent = new Vue({
    el: '#update-content',
        data: {
        message: @Html.DisplayFor(model => model.Updated)
    }
})