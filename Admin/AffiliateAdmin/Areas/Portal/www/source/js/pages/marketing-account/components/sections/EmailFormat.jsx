import React from 'react';
import { Form, FormGroup, Label, Input, Col } from 'reactstrap';
import useNewMarketingAccountData from '../../hooks/useNewMarketingAccountData';

export const EmailFormat = (props) => {

    const { generalInfo, setGeneralInfo } = props;

    const _handleChange = (isTextFormat) => {
        setGeneralInfo(prevState => ({
            ...prevState,
            mailOption: isTextFormat,
        }));
    }

    return (
        <fieldset>
            <legend><h6 className="font-weight-bold">Email Format Preference</h6></legend>
            <Form tag="div">
                <FormGroup row>
                    <Col sm={5}>
                        <FormGroup check className="d-inline-block mr-3">
                            <Label check>
                                <Input name='mailOption' type="radio" checked={generalInfo.mailOption} onChange={() => _handleChange(true)} />{' '} Plain Text Format
                            </Label>
                        </FormGroup>
                        <FormGroup check className="d-inline-block">
                            <Label check>
                                <Input name='mailOption' type="radio" checked={!generalInfo.mailOption} onChange={() => _handleChange(false)} />{' '} Html Format
                            </Label>
                        </FormGroup>
                    </Col>
                </FormGroup>
            </Form>
        </fieldset>
    );
}

export default React.memo(EmailFormat);
