import static com.kms.katalon.core.checkpoint.CheckpointFactory.findCheckpoint
import static com.kms.katalon.core.testcase.TestCaseFactory.findTestCase
import static com.kms.katalon.core.testdata.TestDataFactory.findTestData
import static com.kms.katalon.core.testobject.ObjectRepository.findTestObject
import static com.kms.katalon.core.testobject.ObjectRepository.findWindowsObject
import com.kms.katalon.core.checkpoint.Checkpoint as Checkpoint
import com.kms.katalon.core.cucumber.keyword.CucumberBuiltinKeywords as CucumberKW
import com.kms.katalon.core.mobile.keyword.MobileBuiltInKeywords as Mobile
import com.kms.katalon.core.model.FailureHandling as FailureHandling
import com.kms.katalon.core.testcase.TestCase as TestCase
import com.kms.katalon.core.testdata.TestData as TestData
import com.kms.katalon.core.testobject.TestObject as TestObject
import com.kms.katalon.core.webservice.keyword.WSBuiltInKeywords as WS
import com.kms.katalon.core.webui.keyword.WebUiBuiltInKeywords as WebUI
import com.kms.katalon.core.windows.keyword.WindowsBuiltinKeywords as Windows
import internal.GlobalVariable as GlobalVariable
import org.openqa.selenium.Keys as Keys

WebUI.openBrowser('')

WebUI.navigateToUrl('http://localhost:3000/login')

WebUI.setText(findTestObject('Object Repository/Page_React App/input_username_username'), 'user')

WebUI.setEncryptedText(findTestObject('Object Repository/Page_React App/input_Password_password'), 'bTVIq92haJs=')

WebUI.sendKeys(findTestObject('Object Repository/Page_React App/input_Password_password'), Keys.chord(Keys.ENTER))

WebUI.click(findTestObject('Object Repository/Page_React App/img_Choose images_profilePicture'))
assert (WebUI.getUrl().equals("http://localhost:3000/user/user"));

WebUI.verifyElementPresent(findTestObject('Object Repository/Page_React App/h5_testdisplay'), 0)

WebUI.verifyElementPresent(findTestObject('Object Repository/Page_React App/h6_user'), 0)

WebUI.verifyElementPresent(findTestObject('Object Repository/Page_React App/img_user_profilePicture'), 0)

WebUI.verifyElementPresent(findTestObject('Object Repository/Page_React App/p_test bio hoi ho'), 0)

WebUI.closeBrowser()

