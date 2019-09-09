import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button } from 'reactstrap';

import { resource } from '../modules/constants/selectAll';

export default class SelectAll extends PureComponent {
    constructor(props) {
        super(props);
    }

    _onCheckAll = (isCheckAll) => {
        const { checkedList } = this.props;
        const newCheckedList = Object.keys(checkedList).reduce((accumulator, currentValue) => {
            accumulator[currentValue] = isCheckAll;

            return accumulator;
        }, {});

        this.props.setShouldResetScrollPosition(false);
        this.props.setCheckedList(newCheckedList);
    }

    render() {
        return (
            <div className="selection">
                <span>{resource.Select}</span>
                <Button type="button" color="link" className="text-dark font-weight-bold" onClick={() => this._onCheckAll(true)}>
                    <u>{resource.All}</u>
                </Button>
                <Button type="button" color="link" className="text-dark font-weight-bold" onClick={() => this._onCheckAll(false)}>
                    <u>{resource.None}</u>
                </Button>
            </div>
        );
    }
}