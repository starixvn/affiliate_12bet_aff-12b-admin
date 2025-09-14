import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Row, Col, Card, CardBody, Button } from 'reactstrap';
import ModuleComponentsSection from '../modules/ModuleComponentsSection';

const ModuleComponents = ({  }) => {
    return (
        <Row>
            <Col md={12}>
                <fieldset className='module-components'>
                    <legend><h6 className="font-weight-bold text-success">Module Components</h6></legend>
                    <ModuleComponentsSection />
                </fieldset>
            </Col>
        </Row>
    );
};

function mapStateToProps({  }) {
    return Object.assign({}, {  });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({  }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(ModuleComponents));
