import React, { useState } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';

import { setSelectedTargetCountryId, fetchData } from '../actions';
import { headerText, globalResource } from '../libs/constants';

export const Filter = (props) => {
    const { targetCountryList } = props;
    const [targetCountryId, setTargetCountryId] = useState('');

    const _handleSearch = () => {
        props.setSelectedTargetCountryId(targetCountryId);
        props.fetchData(targetCountryId);
    }

    return targetCountryList && (
        <div className="filter-container my-2">
            <Form inline tag="div">
                <FormGroup className="mr-sm-3">
                    <Label for="targetCountryId" className="mr-sm-2">{headerText.TargetCountry}</Label>
                    <Input type="select" id="targetCountryId" value={targetCountryId} onChange={(e) => setTargetCountryId(e.target.value)}>
                        <option key={'target-country-all'} value={''}>{globalResource.All}</option>
                        {
                            targetCountryList.map(item => {
                                return (<option key={`target-country-${item.TargetCountryId}`} value={item.TargetCountryId}>{item.TargetCountryName}</option>)
                            })
                        }
                    </Input>
                </FormGroup>
                <Button outline color="primary" type="button" onClick={_handleSearch}>
                    <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                </Button>
            </Form>
        </div>
    )
}

function mapStateToProps({ targetCountryList }) {
    return Object.assign({}, { targetCountryList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedTargetCountryId, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Filter));