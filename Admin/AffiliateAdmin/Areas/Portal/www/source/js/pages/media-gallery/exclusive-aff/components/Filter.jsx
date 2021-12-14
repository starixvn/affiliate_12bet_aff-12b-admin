import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { setParam, setIsFromAnotherPage } from '../actions';
import { headerText } from '../libs/constants';

export const Filter = (props) => {
    const { pagingOption, isFromAnotherPage } = props;

    const [affName, setAffName] = useState('');
    const [exclusiveHtmlFile, setExclusiveHtmlFile] = useState(props.exclusiveHtmlFile);

    useEffect(() => {
        if (isFromAnotherPage) {
            _handleSearch();
            props.setIsFromAnotherPage(false);
        }
    }, [isFromAnotherPage]);

    const _handleSearch = () => {
        const newParam = Object.assign({}, {
            AffName: affName,
            ExclusiveHtmlFile: exclusiveHtmlFile,
            PageIndex: 1,
            PageSize: pagingOption.PageSize,
        });

        props.setParam(newParam);
    }

    const _handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            _handleSearch();
        }
    }

    return (
        <div className="filter-container my-2">
            <Form inline tag="div">
                <FormGroup className="mr-sm-3">
                    <Label className="mr-2">{headerText.Affiliate}</Label>
                    <Input type="text" name="affName" value={affName} onKeyPress={_handlePressEnter} onChange={(e) => setAffName(e.target.value)} />
                </FormGroup>
                <FormGroup className="mr-sm-3">
                    <Label className="mr-2">{headerText.ExclusiveHtmlFile}</Label>
                    <Input type="text" name="exclusiveHtmlFile" value={exclusiveHtmlFile} onKeyPress={_handlePressEnter} onChange={(e) => setExclusiveHtmlFile(e.target.value)} />
                </FormGroup>
                <Button outline color="primary" type="button" onClick={_handleSearch}>
                    <span className="oi oi-magnifying-glass"></span> {headerText.Search}
                </Button>
            </Form>
        </div>
    )
}

function mapStateToProps({ pagingOption, affName, isFromAnotherPage }) {
    return Object.assign({}, { pagingOption, affName, isFromAnotherPage });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, setIsFromAnotherPage }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Filter));