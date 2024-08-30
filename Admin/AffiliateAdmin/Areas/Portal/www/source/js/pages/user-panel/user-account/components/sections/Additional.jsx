import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';

import { renderInputForm, capitalizeFirstChar, renderClaimBaseSelectionForm, renderClaimBaseInputForm } from '../../libs/service';
import { countriesList } from '../../libs/constants';
import { setUserAccountInfo } from '../../actions';
import { claimResource } from '../../../../../modules/claim';

export class Additional extends PureComponent {
    constructor(props) {
        super(props);
    }

    handleChange = (event) => {
        const target = event.target;
        const name = capitalizeFirstChar(target.name);

        const newUserAccountInfo = Object.assign({}, this.props.userAccountInfo, { [name]: target.value })
        this.props.setUserAccountInfo(newUserAccountInfo);
    }

    _renderCountry(country, isDisabled) {
        return (
            <FormGroup row>
                <Label for="country" sm={3} xl={2} className="required">Country</Label>
                <Col sm={9} xl={10}>
                    <Input type="select" id="country" name="country" disabled={isDisabled} value={country} onChange={this.handleChange}>
                        <option value="">Select One</option>
                        {
                            Object.keys(countriesList).map((key) => {
                                return (<option key={'country-' + key} value={key}>{countriesList[key]}</option>)
                            })
                        }
                    </Input>
                </Col>
            </FormGroup>
        )
    }

    render() {
        const { userAccountInfo, userClaims } = this.props;
        const { hasWriteEditAffClaim } = userClaims;

        return (
            <Form tag="div">
                {renderInputForm('address', 'Address', userAccountInfo.Address, !hasWriteEditAffClaim, true, this.handleChange)}
                {renderInputForm('city', 'City', userAccountInfo.City, !hasWriteEditAffClaim, true, this.handleChange)}
                {renderInputForm('state', 'State', userAccountInfo.State, !hasWriteEditAffClaim, false, this.handleChange)}
                {renderInputForm('postcode', 'Postcode/Zip', userAccountInfo.Postcode, !hasWriteEditAffClaim, true, this.handleChange)}
                {this._renderCountry(userAccountInfo.Country, !hasWriteEditAffClaim)}
                {renderClaimBaseSelectionForm(userClaims, claimResource.SecurityQA, 'questionId', 'Security Question', userAccountInfo.SecurityQuestions, userAccountInfo.QuestionId, false, this.handleChange, userAccountInfo.Question)}
                {renderClaimBaseInputForm(userClaims, claimResource.SecurityQA, 'answer', 'Answer', userAccountInfo.Answer, false, this.handleChange)}
            </Form>
        )
    }
}

function mapStateToProps({ edit, claimsPrincipal }) {
    const { userAccountInfo, hasWriteEditAffClaim, hasReadManagePendingAffClaim, hasWriteManagePendingAffClaim } = edit;
    const userClaims = { claimsPrincipal, hasWriteEditAffClaim, hasReadManagePendingAffClaim, hasWriteManagePendingAffClaim }

    return Object.assign({}, { userAccountInfo, userClaims });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setUserAccountInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Additional);