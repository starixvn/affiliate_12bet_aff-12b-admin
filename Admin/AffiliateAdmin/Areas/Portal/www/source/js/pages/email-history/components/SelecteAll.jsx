import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button } from 'reactstrap';

import { resource } from '../modules/constants';
import { setCheckedEmails, setShouldResetScrollPosition } from '../actions';

export class SelecteAll extends PureComponent {
    constructor(props) {
        super(props);
    }

    onCheckAll = (isCheckAll) => {
        const { checkedEmails } = this.props;
        const newCheckedEmails = Object.keys(checkedEmails).reduce((accumulator, currentValue) => {
            accumulator[currentValue] = isCheckAll;

            return accumulator;
        }, {});

        this.props.setShouldResetScrollPosition(false);
        this.props.setCheckedEmails(newCheckedEmails);
    }

    render() {
        return (
            <div className="selection">
                <span>{resource.Select}</span>
                <Button type="button" color="link" className="text-dark font-weight-bold" onClick={() => this.onCheckAll(true)}>
                    <u>All</u>
                </Button>
                <Button type="button" color="link" className="text-dark font-weight-bold" onClick={() => this.onCheckAll(false)}>
                    <u>None</u>
                </Button>
            </div>
        );
    }
}


function mapStateToProps({ checkedEmails }) {
    return Object.assign({}, { checkedEmails });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setCheckedEmails, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(SelecteAll);