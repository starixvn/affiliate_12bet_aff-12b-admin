import React, { PureComponent } from 'react';
import { connect } from 'react-redux';

export class Result extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            result: null
        }
    }

    render() {
        const { result } = this.state;

        return (
            <div className="result-container">
                result info
            </div>
        );
    }
}

function mapStateToProps({ file }) {
    return Object.assign({}, { file });
}

export default connect(mapStateToProps)(Result);