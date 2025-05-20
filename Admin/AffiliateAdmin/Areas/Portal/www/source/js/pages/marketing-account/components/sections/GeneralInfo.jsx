import React from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { FormGroup, Label, Input, Col } from 'reactstrap';

import { setAffId } from '../../actions';

export const GeneralInfo = (props) => {

    const { generalInfo, setGeneralInfo } = props;

    const _handleChange = (e) => {
        setGeneralInfo({
            ...generalInfo,
            [e.target.name]: e.target.value
        });
    }

    // TODO: Need Review
    const _handleMarketingAccountChange = (e) => {
        props.setAffId(e.target.value);
    }

    return (
        <div>
            <FormGroup row>
                <Label htmlFor="accountName" sm={3}>Copy from Marketing account</Label>
                <Col sm={6}>
                    <Input type="select" id="marketingAccount"
                        value={props.affId}
                        onChange={_handleMarketingAccountChange}>
                        <option value="927841">927841</option>
                        <option value="1166364">1166364</option>
                        <option value="1185988">1185988</option>
                    </Input>
                </Col>
            </FormGroup>
            <FormGroup row>
                <Label htmlFor="targetCountryCode" sm={3}>Target Country</Label>
                <Col sm={6}>
                    <Input type="select" name="targetCountryCode" id="targetCountryCode"
                        value={generalInfo.targetCountryCode}
                        onChange={_handleChange}>
                        <option value="">Select One</option>
                        {
                            Object.keys(props.targetCountryCodeList).map(key => {
                                return (<option key={`targetCountryCode-${key}`} value={key}>{props.targetCountryCodeList[key]}</option>)
                            })
                        }
                    </Input>
                </Col>
            </FormGroup>
            <FormGroup row>
                <Label htmlFor="language" sm={3}>Language</Label>
                <Col sm={6}>
                    <Input type="select" name="language" id="language"
                        value={generalInfo.language}
                        onChange={_handleChange}>
                        <option value="">Select One</option>
                        {
                            Object.keys(props.languageCodeList).map(key => {
                                return (<option key={`language-${key}`} value={key}>{props.languageCodeList[key]}</option>)
                            })
                        }
                    </Input>
                </Col>
            </FormGroup>
        </div>
    );
};

const mapStateToProps = ({ affId, languageCodeList, targetCountryCodeList }) => {
    return {
        affId,
        languageCodeList,
        targetCountryCodeList
    }
}

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        setAffId
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(GeneralInfo));
