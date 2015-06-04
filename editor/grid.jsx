var GameBrick = React.createClass({
    getInitialState: function() {
        return {
            empty: true,
            resistance: 0
        };
    },

    getDefaultProps: function() {
        return {
            line: 0,
            column: 0,
            maxResistance: 4
        };
    },

    change: function() {
        if (this.state.resistance < this.props.maxResistance) {
            this.setState({resistance: this.state.resistance + 1});
        }
        else {
            this.setState({resistance: 0});
        }
    },

    render: function() {
        return (
            <img src={"img/brick" + this.state.resistance + ".png"} onClick={this.change} />
        );
    }
});

var GameGrid = React.createClass({
    getDefaultProps: function() {
        return {
            lines: 20,
            columns: 16
        };
    },

    render: function() {
        var rows = [];
        for (var line=0; line < this.props.lines; line++) {
            var row = [];
            for (var column=0; column < this.props.columns; column++) {
                row.push(<td key={column}><GameBrick line={line} column={column} /></td>);
            }
            rows.push(<tr key={line}>{row}</tr>);
        }

        return (
            <table className="table table-bordered table-hover">
              {rows}
            </table>
        );
    }
});
