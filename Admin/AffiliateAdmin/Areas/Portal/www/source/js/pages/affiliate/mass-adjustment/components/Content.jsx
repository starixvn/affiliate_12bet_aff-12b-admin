import React, { PureComponent } from 'react';
import { connect } from 'react-redux';

import Note from './Note';
import UploadResult from './UploadResult';

export class Content extends PureComponent {
    constructor(props) {
        super(props);
    }

    render() {
        const { isShowResult } = this.props;

        return (
            <div className="mass-adjustment-content">
                {
                    isShowResult ? <UploadResult /> : <Note />
                }
            </div>
        );
    }
}

function mapStateToProps({ isShowResult }) {
    return Object.assign({}, { isShowResult });
}

export default connect(mapStateToProps)(Content);