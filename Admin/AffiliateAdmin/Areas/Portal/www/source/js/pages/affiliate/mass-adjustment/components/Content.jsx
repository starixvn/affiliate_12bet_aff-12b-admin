import React, { PureComponent } from 'react';
import { connect } from 'react-redux';

import Note from './Note';
import Result from './Result';

export class Content extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const { file } = this.props;

        return (
            <div className="mass-adjustment-content">
                {
                    !file ? <Note /> : <Result />
                }
            </div>
        );
    }
}

function mapStateToProps({ file }) {
    return Object.assign({}, { file });
}

export default connect(mapStateToProps)(Content);