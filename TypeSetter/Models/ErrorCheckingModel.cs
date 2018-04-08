using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;

namespace TypeSetter.Models
{
    public class ErrorCheckingModel
    {
        
        int flag = 0;
        String openTag = @"^([A-z]{1,6}(_#){0,1}[A-z]{0,6}[0-9]{0,6}[*]{1})$";
        String closeTag = @"^([*]{1}[A-z]{1,6}(_#){0,1}[A-z]{0,6}[0-9]{0,6})$";
        
        //List of tags
        List<String> listOfOPENmetadataTag = new List<String> { "/bs*/", "/is*/", "/us*/", "/ss*/", "/h1*/", "/h2*/", "/h3*/", "/h4*/", "/h5*/", "/h6*/", "/ah1*/", "/ah2*/", "/ul*/", "/ol*/", "/usb*/", "/osl*/", "/pbreak*/", "/hurl*/", "/hlabel*/", "/fc_#FF0000*/", "/taL*/", "/taC*/", "/taR*/", "/taJ*/", "/bq*/", "/th*/" };
        List<String> listOfCLOSEmetadataTag = new List<String> { "/*be/", "/*ie/", "/*ue/", "/*se/", "/*h1/", "/*h2/", "/*h3/", "/*h4/", "/*h5/", "/*h6/", "/*ah1/", "/*ah2/", "/*ul/", "/*ol/", "/*usb/", "/*osl/", "/*hurl/", "/*hlabel/", "/*fc_#FF0000/", "/*taL/", "/*taC/", "/*taR/", "/*taJ/", "/*bq/", "/*th/" };
        
        //constructor
        public ErrorCheckingModel() {
           
        }
        Stack<string> myStack = new Stack<string>(); //specify the datatype here

        
        //Check for error flag
        //when stack is empty top equal to minus one
        //max size to give the maximum number of elements that can be held by the stack
        //need to check whether there is any space in the stack for new item
        //if top = maxsize - 1 (Stack overflow)
        //else top = top + 1;
        //set stack[top] = item <-- index
        //put the number at the index 0
        
        //<---- pop -->
        // if (top = -1) <-- no element in the stack <-- stack underflow
        //set item = stack[top] <-- deleting
        // set top = top - 1
        //pop does not need to pass any element as argument because pop function always remove the top element from the stack
        public int CheckErrorInContent(string contentToCheck)
        {
         
            List<string> metaData = new List<string>();
           
            //Console.WriteLine(contentToCheck);
            String[] line = contentToCheck.Split("\n");
            if (line != null)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    String[] lineSplit = line[i].Split("/");
                    String tempString = "";
                    for (int j = 0; j < lineSplit.Length; j++)
                    {
                        //TO DETECT THE OPENING TAG
                        Match openTagResult = Regex.Match(lineSplit[j], openTag);
                        //TO DETECT THE CLOSING TAG
                        Match closeTagResult = Regex.Match(lineSplit[j], closeTag);
            
                        if (openTagResult.Success) {
                            //replace the text
                            tempString = "/" + lineSplit[j] + "/";

                            if (listOfOPENmetadataTag.Contains(tempString))
                            {
                                if (tempString == "/bs*/")
                                {
                                    myStack.Push(tempString);
                                    String openingTag6 = Regex.Replace(tempString, @"bs\*", "b");
                                    metaData.Add(openingTag6);
                                    
                                }
                                else if (tempString == "/is*/")
                                {
                                    myStack.Push(tempString);
                                    String openingTag1 = Regex.Replace(tempString, @"is\*", "i");
                                    metaData.Add(openingTag1);
                                    
                                }
                                
                                else if (tempString == "/us*/")
                                {
                                    myStack.Push(tempString);
                                    String openingTag7 = Regex.Replace(tempString, @"us\*", "u");
                                    metaData.Add(openingTag7);
                                    
                                }
                                
                                else if (tempString == "/ss*/")
                                {
                                    myStack.Push(tempString);
                                    String openingTag8 = Regex.Replace(tempString, @"ss\*", "s");
                                    metaData.Add(openingTag8);
                                    
                                }
                              
                                //check for pbreak; cause there is no closing tag
                                else if (tempString != "/pbreak*/")
                                {
                                    myStack.Push(tempString);
                                    String removeSlashTag = Regex.Replace(tempString, @"\*", "");
                                    metaData.Add(removeSlashTag);

                                }
                                
                            }
                            //typeSetter does not support
                            else
                            {
                                flag = 1;
                            }

                        }
                        else if (closeTagResult.Success)
                            {
                                tempString = "/" + lineSplit[j] + "/";
                                //Console.WriteLine(tempString);

                                if (listOfCLOSEmetadataTag.Contains(tempString))
                                {    
                                    //pop out the opening tag
                                    //check that stack is not empty
                                    String topOfStack;
                               
                                    if (myStack.Count != 0)
                                    {

                                        if (tempString == "/*ie/")
                                        {
                                            
                                            topOfStack = myStack.Pop();
                                            String closeTag1 = Regex.Replace(tempString, @"\*ie", "i");
                                            //Console.WriteLine(closeTag);
                                            String openingTag1 = Regex.Replace(topOfStack, @"is\*", "i");
                                            //Console.WriteLine(openingTag);
                                            if (openingTag1 == closeTag1)
                                            {
                                                flag = 0;
                                                //Console.WriteLine("same tag");       
                                                foreach (var item in metaData.ToList())
                                                {
                                                    
                                                    if (item == closeTag1)
                                                    {
                                                        metaData.Remove(item);
                                                        //Console.WriteLine(item);
                                                        break;
                                                    }
                                                }
                                            }
                                            else if (openingTag1 != closeTag1)
                                            {
                                      
                                                //closing and opening not tally
                                               flag = 1;
                                            }
                                        }
                                        
                                        else if (tempString == "/*ue/")
                                        {
                                            //Console.Write("ohno");
                                            topOfStack = myStack.Pop();
                                            String closeTag4 = Regex.Replace(tempString, @"\*ue", "u");
                                            //Console.WriteLine(closeTag3);
                                            String openingTag4 = Regex.Replace(topOfStack, @"us\*", "u");
                                            //Console.WriteLine(openingTag3);
                                            if (openingTag4 == closeTag4)
                                            {
                                                flag = 0;
                                                //Console.WriteLine("same tag");       
                                                foreach (var item in metaData.ToList())
                                                {
                                                    
                                                    if (item == closeTag4)
                                                    {
                                                        metaData.Remove(item);
                                                        //Console.WriteLine(item);
                                                        break;
                                                    }
                                                }
                                            }
                                            else if (openingTag4 != closeTag4)
                                            {
                                        
                                                //closing and opening not tally
                                                flag = 1;
                                            }
                                        }
                                        
                                        else if (tempString == "/*be/")
                                        {
                                            //Console.Write("ohno");
                                            topOfStack = myStack.Pop();
                                            String closeTag3 = Regex.Replace(tempString, @"\*be", "b");
                                            //Console.WriteLine(closeTag3);
                                            String openingTag3 = Regex.Replace(topOfStack, @"bs\*", "b");
                                            //Console.WriteLine(openingTag3);
                                            if (openingTag3 == closeTag3)
                                            {
                                                flag = 0;
                                                //Console.WriteLine("same tag");       
                                                foreach (var item in metaData.ToList())
                                                {
                                                    
                                                    if (item == closeTag3)
                                                    {
                                                        metaData.Remove(item);
                                                        //Console.WriteLine(item);
                                                        break;
                                                    }
                                                }
                                            }
                                            else if (openingTag3 != closeTag3)
                                            {
                                        
                                                //closing and opening not tally
                                                flag = 1;
                                            }
                                        }
                                        
                                        else if (tempString == "/*se/")
                                        {
                                          
                                            topOfStack = myStack.Pop();
                                            String closeTag5 = Regex.Replace(tempString, @"\*se", "s");
                                           
                                            String openingTag5 = Regex.Replace(topOfStack, @"ss\*", "s");
                                            
                                            if (openingTag5 == closeTag5)
                                            {
                                                flag = 0;
                                                //Console.WriteLine("same tag");       
                                                foreach (var item in metaData.ToList())
                                                {
                                                    
                                                    if (item == closeTag5)
                                                    {
                                                        metaData.Remove(item);
                                                        //Console.WriteLine(item);
                                                        break;
                                                    }
                                                }
                                            }
                                            else if (openingTag5 != closeTag5)
                                            {
                                        
                                                //closing and opening not tally
                                                flag = 1;
                                            }
                                        }
                                       
                                      else if (tempString != "/*be/" || tempString != "/*ie/" || tempString != "/*ue/" || tempString != "/*se/") {
                                        topOfStack = myStack.Pop();
                                        String closeTag2 = Regex.Replace(tempString, @"\*", "");
                                        //Console.WriteLine(closeTag2);
                                        String openingTag2 = Regex.Replace(topOfStack, @"\*", "");
                                        //Console.WriteLine(openingTag);
                                        if (openingTag2 == closeTag2)
                                        {
                                            flag = 0;
                                            //Console.WriteLine("same tag");       
                                            foreach (var item in metaData.ToList())
                                            {
                                                if (item == closeTag2)
                                                {
                                                    metaData.Remove(item);
                                                    //Console.WriteLine(item);
                                                    break;
                                                }
                                            }
                                        }
                                        else if (openingTag2 != closeTag2)
                                        {
                          
                                             flag = 1;
                                        }
                                    }
                                }
                                   //extra closing tag
                                    else 
                                    {
                                        flag = 1;
                                    }
                                }
                                //typeSetter does not support
                                else
                                {
                                    flag = 1;
                                }
                              
                            }                
                        //break out of the loop
                        if (flag == 1)
                            break;               
                    }         
                }
                foreach (var o in metaData)
                {
                    //opening tag is not closed
                    //extra opening tag
                    if (metaData.Any())
                    {
                        flag = 1;
                    }
                }
            }
            return flag;
        }
    }
}
