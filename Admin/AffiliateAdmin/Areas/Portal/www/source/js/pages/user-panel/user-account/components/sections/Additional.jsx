import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Col, Form, FormGroup, Label, Input } from 'reactstrap';

import { renderInputForm, capitalizeFirstChar, renderClaimBaseSelectionForm, renderClaimBaseInputForm } from '../../modules/service';
import { countriesList } from '../../modules/constants';
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

    _renderCountry(country) {
        return (
            <FormGroup row>
                <Label for="country" sm={3} xl={2} className="required">Country</Label>
                <Col sm={9} xl={10}>
                    <Input type="select" id="country" name="country" value={country} onChange={this.handleChange}>
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
        const { userAccountInfo, claimsPrincipal } = this.props;

        return userAccountInfo && (
            <Form tag="div">
                {renderInputForm('address', 'Address', userAccountInfo.Address, false, true, this.handleChange)}
                {renderInputForm('city', 'City', userAccountInfo.City, false, true, this.handleChange)}
                {renderInputForm('state', 'State', userAccountInfo.State, false, false, this.handleChange)}
                {renderInputForm('postcode', 'Postcode/Zip', userAccountInfo.Postcode, false, true, this.handleChange)}
                {this._renderCountry(userAccountInfo.Country)}
                {renderClaimBaseSelectionForm(claimsPrincipal, claimResource.SecurityQuestion, 'questionId', 'Security Question', userAccountInfo.SecurityQuestions, userAccountInfo.QuestionId,  false, this.handleChange, userAccountInfo.Question)}
                {renderClaimBaseInputForm(claimsPrincipal, claimResource.SecurityQuestion, 'answer', 'Answer', userAccountInfo.Answer, false, this.handleChange)}
            </Form>
        )
    }
}

function mapStateToProps({ edit, claimsPrincipal }) {
    const { userAccountInfo } = edit;
    return Object.assign({}, { userAccountInfo, claimsPrincipal });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setUserAccountInfo }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Additional);