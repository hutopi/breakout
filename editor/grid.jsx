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
            column: 0
        };
    },

    change: function() {
        var value = React.findDOMNode(this.refs.input).value;
        if (value !== '') {
            this.setState({
                empty: false,
                resistance: parseInt(value)
            });
        }
        else {
            this.setState({
                empty: true,
                resistance: 0
            })
        }
    },

    render: function() {
        return (
            <input type="text" size="1" maxLength="1" onInput={this.change} ref="input" />
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
