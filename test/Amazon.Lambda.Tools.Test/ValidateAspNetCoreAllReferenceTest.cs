﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Xunit;

using Amazon.Lambda;

namespace Amazon.Lambda.Tools.Test
{
    public class ValidateAspNetCoreAllReferenceTest
    {
        [Fact]
        public void NewerAspNetCoreReference()
        {
            var logger = new TestToolLogger();
            var manifest = File.ReadAllText(@"ManifestTestFiles/SampleManifest.xml");
            var projectFile = File.ReadAllText(@"ManifestTestFiles/NewerAspNetCoreReference.xml");

            Assert.Throws<LambdaToolsException>(() => LambdaUtilities.ValidateMicrosoftAspNetCoreAllReferenceWithManifest(logger, manifest, projectFile));
        }

        [Fact]
        public void CurrentAspNetCoreReference()
        {
            var logger = new TestToolLogger();
            var manifest = File.ReadAllText(@"ManifestTestFiles/SampleManifest.xml");
            var projectFile = File.ReadAllText(@"ManifestTestFiles/CurrentAspNetCoreReference.xml");

            LambdaUtilities.ValidateMicrosoftAspNetCoreAllReferenceWithManifest(logger, manifest, projectFile);

            Assert.DoesNotContain("error", logger.Buffer.ToLower());
        }

        [Fact]
        public void NotUsingAspNetCore()
        {
            var logger = new TestToolLogger();
            var manifest = File.ReadAllText(@"ManifestTestFiles/SampleManifest.xml");
            var projectFile = File.ReadAllText(@"ManifestTestFiles/CurrentAspNetCoreReference.xml");

            LambdaUtilities.ValidateMicrosoftAspNetCoreAllReferenceWithManifest(logger, manifest, projectFile);

            Assert.DoesNotContain("error", logger.Buffer.ToLower());
        }

        [Theory]
        [InlineData(@"ManifestTestFiles/ProjectFilesAspNetCoreAllValidation/csharp")]
        [InlineData(@"ManifestTestFiles/ProjectFilesAspNetCoreAllValidation/fsharp")]
        [InlineData(@"ManifestTestFiles/ProjectFilesAspNetCoreAllValidation/vb")]
        public void FindProjFiles(string projectDirectory)
        {
            var logger = new TestToolLogger();
            string manifest;

            LambdaUtilities.ValidateMicrosoftAspNetCoreAllReference(logger, projectDirectory, out manifest);

            Assert.DoesNotContain("error", logger.Buffer.ToLower());
        }
    }
}
