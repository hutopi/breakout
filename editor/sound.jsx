var Sound = React.createClass({
    getInitialState: function() {
        return {
            file: null,
            type: null
        }
    },

    change: function() {
        var reader = new FileReader;
        reader.onloadend = function() {
            this.setState({file: btoa(reader.result)});
        }.bind(this);

        var file = React.findDOMNode(this.refs.input).files[0]
        reader.readAsBinaryString(file);
        this.setState({type: file.type});
    },

    render: function() {
        var sound = null;
        if (this.state.type != null) {
            sound = <audio controls src={'data:' + this.state.type + ';base64,' + this.state.file} />;
        }

        return (
            <div>
                { sound }
                <br />
                <input type="file" ref="input" onChange={this.change} />
            </div>
        );
    },

    export: function () {
        return {
            file: this.state.file,
            type: this.state.type
        }
    }
});